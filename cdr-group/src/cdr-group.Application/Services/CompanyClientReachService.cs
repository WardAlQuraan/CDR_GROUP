using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using cdr_group.Application.Helpers;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyClientReach;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyClientReachService : BaseService<CompanyClientReach, CompanyClientReachDto, CreateCompanyClientReachDto, UpdateCompanyClientReachDto>, ICompanyClientReachService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string LogoFolder = "uploads/client-reaches";

        public CompanyClientReachService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override IRepository<CompanyClientReach> Repository => UnitOfWork.CompanyClientReaches;

        public override async Task<PagedResult<CompanyClientReachDto>> GetPagedAsync(PagedRequest request)
        {
            var reachRequest = request as CompanyClientReachPagedRequest ?? new CompanyClientReachPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (reaches, totalCount) = await UnitOfWork.CompanyClientReaches.GetCompanyClientReachesPagedAsync(reachRequest);
            var dtos = Mapper.Map<List<CompanyClientReachDto>>(reaches);
            return new PagedResult<CompanyClientReachDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyClientReachDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyClientReaches.GetWithCompanyAsync(id);
            return Mapper.Map<CompanyClientReachDto>(entity);
        }

        public async Task<IEnumerable<CompanyClientReachDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var reaches = await UnitOfWork.CompanyClientReaches.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyClientReachDto>>(reaches);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyClientReachDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyClientReachDto dto, CompanyClientReach entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        public async Task<string> UploadLogoAsync(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new InvalidOperationException("No file uploaded.");
            }

            var clientReach = await UnitOfWork.CompanyClientReaches.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"{nameof(CompanyClientReach)} not found.");

            if (!string.IsNullOrEmpty(clientReach.ClientLogoUrl))
            {
                var oldRelative = ToRelativePath(clientReach.ClientLogoUrl);
                var oldAbsolutePath = Path.Combine(_webHostEnvironment.WebRootPath, oldRelative);
                if (File.Exists(oldAbsolutePath))
                {
                    File.Delete(oldAbsolutePath);
                }
            }

            var extension = Path.GetExtension(file.FileName);
            var storedFileName = $"{Guid.NewGuid()}{extension}";
            var relativePath = Path.Combine(LogoFolder, storedFileName).Replace("\\", "/");
            var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

            var directory = Path.GetDirectoryName(absolutePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fullUrl = UrlHelper.BuildFullUrl(relativePath, _httpContextAccessor) ?? relativePath;
            clientReach.ClientLogoUrl = fullUrl;
            await UnitOfWork.CompanyClientReaches.UpdateAsync(clientReach);
            await UnitOfWork.SaveChangesAsync();

            return fullUrl;
        }

        private static string ToRelativePath(string urlOrPath)
        {
            if (Uri.TryCreate(urlOrPath, UriKind.Absolute, out var uri))
            {
                return uri.AbsolutePath.TrimStart('/');
            }
            return urlOrPath.TrimStart('/');
        }
    }
}
