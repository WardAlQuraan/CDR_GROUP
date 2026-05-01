using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using cdr_group.Application.Helpers;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.CompanyBranch;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyBranchService : BaseService<CompanyBranch, CompanyBranchDto, CreateCompanyBranchDto, UpdateCompanyBranchDto>, ICompanyBranchService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string ImageFolder = "uploads/branches";

        public CompanyBranchService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override IRepository<CompanyBranch> Repository => UnitOfWork.CompanyBranches;

        public override async Task<PagedResult<CompanyBranchDto>> GetPagedAsync(PagedRequest request)
        {
            var branchRequest = request as CompanyBranchPagedRequest ?? new CompanyBranchPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            var (branches, totalCount) = await UnitOfWork.CompanyBranches.GetCompanyBranchesPagedAsync(branchRequest);
            var dtos = Mapper.Map<List<CompanyBranchDto>>(branches);
            return new PagedResult<CompanyBranchDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<CompanyBranchDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.CompanyBranches.GetWithRelationsAsync(id);
            return Mapper.Map<CompanyBranchDto>(entity);
        }

        public async Task<IEnumerable<CompanyBranchDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var branches = await UnitOfWork.CompanyBranches.GetByCompanyIdAsync(companyId);
            return Mapper.Map<List<CompanyBranchDto>>(branches);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyBranchDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }

            if (!await UnitOfWork.Cities.ExistsAsync(dto.CityId))
            {
                throw new InvalidOperationException("City not found.");
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyBranchDto dto, CompanyBranch entity)
        {
            if (dto.CompanyId.HasValue && !await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }

            if (dto.CityId.HasValue && !await UnitOfWork.Cities.ExistsAsync(dto.CityId.Value))
            {
                throw new InvalidOperationException("City not found.");
            }
        }

        public async Task<string> UploadImageAsync(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new InvalidOperationException("No file uploaded.");
            }

            var branch = await UnitOfWork.CompanyBranches.GetByIdAsync(id)
                ?? throw new InvalidOperationException($"{nameof(CompanyBranch)} not found.");

            if (!string.IsNullOrEmpty(branch.ImageUrl))
            {
                var oldRelative = ToRelativePath(branch.ImageUrl);
                var oldAbsolutePath = Path.Combine(_webHostEnvironment.WebRootPath, oldRelative);
                if (File.Exists(oldAbsolutePath))
                {
                    File.Delete(oldAbsolutePath);
                }
            }

            var extension = Path.GetExtension(file.FileName);
            var storedFileName = $"{Guid.NewGuid()}{extension}";
            var relativePath = Path.Combine(ImageFolder, storedFileName).Replace("\\", "/");
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
            branch.ImageUrl = fullUrl;
            await UnitOfWork.CompanyBranches.UpdateAsync(branch);
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
