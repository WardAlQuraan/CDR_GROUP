using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using cdr_group.Application.Helpers;
using cdr_group.Contracts.DTOs.FileAttachment;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class FileAttachmentService : BaseService<FileAttachment, FileAttachmentDto, CreateFileAttachmentDto, UpdateFileAttachmentDto>, IFileAttachmentService
    {
        private readonly IFileAttachmentRepository _fileAttachmentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string UploadFolder = "uploads";

        public FileAttachmentService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper)
        {
            _fileAttachmentRepository = unitOfWork.FileAttachments;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override IRepository<FileAttachment> Repository => _fileAttachmentRepository;

        public async Task<IEnumerable<FileAttachmentDto>> GetByEntityIdAsync(Guid entityId)
        {
            var files = await _fileAttachmentRepository.GetByEntityIdAsync(entityId);
            var dtos = Mapper.Map<IEnumerable<FileAttachmentDto>>(files);
            return PopulateFileUrls(dtos);
        }

        public async Task<IEnumerable<FileAttachmentDto>> GetByEntityTypeAsync(string entityType)
        {
            var files = await _fileAttachmentRepository.GetByEntityTypeAsync(entityType);
            var dtos = Mapper.Map<IEnumerable<FileAttachmentDto>>(files);
            return PopulateFileUrls(dtos);
        }

        public async Task<IEnumerable<FileAttachmentDto>> GetByEntityAsync(Guid entityId, string entityType)
        {
            var files = await _fileAttachmentRepository.GetByEntityAsync(entityId, entityType);
            var dtos = Mapper.Map<IEnumerable<FileAttachmentDto>>(files);
            return PopulateFileUrls(dtos);
        }

        private IEnumerable<FileAttachmentDto> PopulateFileUrls(IEnumerable<FileAttachmentDto> dtos)
        {
            foreach (var dto in dtos)
            {
                dto.FileUrl = UrlHelper.BuildFullUrl(dto.Path, _httpContextAccessor);
            }
            return dtos;
        }

        protected override async Task BeforeCreateAsync(FileAttachment entity, CreateFileAttachmentDto dto)
        {
            // Remove old files if flag is set and EntityId is provided
            if (dto.RemoveOldFiles && dto.EntityId.HasValue)
            {
                var oldFiles = string.IsNullOrEmpty(dto.EntityType)
                    ? await _fileAttachmentRepository.GetByEntityIdAsync(dto.EntityId.Value)
                    : await _fileAttachmentRepository.GetByEntityAsync(dto.EntityId.Value, dto.EntityType);

                foreach (var oldFile in oldFiles)
                {
                    // Delete physical file
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, oldFile.Path);
                    if (File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }

                    // Soft delete the record
                    await _fileAttachmentRepository.DeleteAsync(oldFile);
                }
            }

            var file = dto.File;

            // Generate unique stored file name
            var fileExtension = Path.GetExtension(file.FileName);
            var storedFileName = $"{Guid.NewGuid()}{fileExtension}";

            // Create subfolder based on entity type or use general uploads folder
            var subFolder = !string.IsNullOrEmpty(dto.EntityType)
                ? dto.EntityType.ToLowerInvariant()
                : "general";

            var relativePath = Path.Combine(UploadFolder, subFolder, storedFileName);
            var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

            // Ensure directory exists
            var directory = Path.GetDirectoryName(absolutePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save file to wwwroot
            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Set entity properties
            entity.FileName = file.FileName;
            entity.StoredFileName = storedFileName;
            entity.Path = relativePath.Replace("\\", "/");
            entity.ContentType = file.ContentType;
            entity.Size = file.Length;

            // If setting as primary, reset other files for same entity
            if (dto.IsPrimary && dto.EntityId.HasValue && !string.IsNullOrEmpty(dto.EntityType))
            {
                await ResetPrimaryFilesAsync(dto.EntityId.Value, dto.EntityType);
            }

            await base.BeforeCreateAsync(entity, dto);
        }

        protected override async Task BeforeUpdateAsync(FileAttachment entity, UpdateFileAttachmentDto dto)
        {
            // If setting as primary, reset other files for same entity
            if (dto.IsPrimary == true && entity.EntityId.HasValue && !string.IsNullOrEmpty(entity.EntityType))
            {
                await ResetPrimaryFilesAsync(entity.EntityId.Value, entity.EntityType, entity.Id);
            }

            await base.BeforeUpdateAsync(entity, dto);
        }

        protected override async Task BeforeDeleteAsync(FileAttachment entity)
        {
            // Delete physical file from wwwroot
            var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, entity.Path);
            if (File.Exists(absolutePath))
            {
                File.Delete(absolutePath);
            }

            await base.BeforeDeleteAsync(entity);
        }

        private async Task ResetPrimaryFilesAsync(Guid entityId, string entityType, Guid? excludeFileId = null)
        {
            var files = await _fileAttachmentRepository.GetByEntityAsync(entityId, entityType);
            foreach (var file in files)
            {
                if (file.IsPrimary && file.Id != excludeFileId)
                {
                    file.IsPrimary = false;
                    await _fileAttachmentRepository.UpdateAsync(file);
                }
            }
        }

        public async Task<IEnumerable<BulkFileOperationResultDto>> BulkOperationAsync(IEnumerable<BulkFileOperationItemDto> items)
        {
            var results = new List<BulkFileOperationResultDto>();

            foreach (var item in items)
            {
                var result = new BulkFileOperationResultDto
                {
                    EntityId = item.EntityId,
                    EntityType = item.EntityType,
                    FileId = item.FileId
                };

                try
                {
                    // If FileId is provided and File is null => Delete
                    if (item.FileId.HasValue && item.File == null)
                    {
                        var existingFile = await _fileAttachmentRepository.GetByIdAsync(item.FileId.Value);
                        if (existingFile != null)
                        {
                            await DeleteAsync(item.FileId.Value);
                            result.Operation = "deleted";
                            result.FileId = item.FileId;
                        }
                        else
                        {
                            result.Operation = "error";
                            result.ErrorMessage = "File not found";
                        }
                    }
                    // If File is provided => Upload (and optionally delete old FileId first)
                    else if (item.File != null)
                    {
                        // Delete old file if FileId is provided
                        if (item.FileId.HasValue)
                        {
                            var existingFile = await _fileAttachmentRepository.GetByIdAsync(item.FileId.Value);
                            if (existingFile != null)
                            {
                                await DeleteAsync(item.FileId.Value);
                            }
                        }

                        // Upload new file
                        var createDto = new CreateFileAttachmentDto
                        {
                            File = item.File,
                            EntityId = item.EntityId,
                            EntityType = item.EntityType,
                            Description = item.Description,
                            RemoveOldFiles = false,
                            IsPrimary = item.IsPrimary
                        };

                        var uploaded = await CreateAsync(createDto);
                        result.Operation = "uploaded";
                        result.FileId = uploaded.Id;
                        result.FilePath = UrlHelper.BuildFullUrl(uploaded.Path, _httpContextAccessor);
                    }
                    else
                    {
                        result.Operation = "skipped";
                        result.ErrorMessage = "No file provided and no fileId to delete";
                    }
                }
                catch (Exception ex)
                {
                    result.Operation = "error";
                    result.ErrorMessage = ex.Message;
                }

                results.Add(result);
            }

            return results;
        }
    }
}
