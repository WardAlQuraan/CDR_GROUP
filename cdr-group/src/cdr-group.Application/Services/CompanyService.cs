using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using cdr_group.Application.Helpers;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Company;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class CompanyService : BaseService<Company, CompanyDto, CreateCompanyDto, UpdateCompanyDto>, ICompanyService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string LogoFolder = "uploads/companies";

        public CompanyService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override IRepository<Company> Repository => UnitOfWork.Companies;

        public override async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            var (companies, _) = await UnitOfWork.Companies.GetCompaniesPagedAsync(new PagedRequest { PageSize = int.MaxValue });
            var dtos = Mapper.Map<List<CompanyDto>>(companies);
            await PopulateCountsAsync(dtos);
            return dtos;
        }

        public override async Task<PagedResult<CompanyDto>> GetPagedAsync(PagedRequest request)
        {
            var (companies, totalCount) = await UnitOfWork.Companies.GetCompaniesPagedAsync(request);
            var companyDtos = Mapper.Map<List<CompanyDto>>(companies);
            await PopulateCountsAsync(companyDtos);
            return new PagedResult<CompanyDto>(companyDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<IEnumerable<CompanyDto>> GetActiveCompaniesAsync()
        {
            var companies = await UnitOfWork.Companies.GetActiveCompaniesAsync();
            var dtos = Mapper.Map<List<CompanyDto>>(companies);
            await PopulateCountsAsync(dtos);
            return dtos;
        }

        public async Task<IEnumerable<CompanyDto>> GetTreeAsync(Guid? parentId = null)
        {
            var companies = await UnitOfWork.Companies.GetRelatedActiveCompaniesAsync(parentId);
            var allDtos = Mapper.Map<List<CompanyDto>>(companies);
            await PopulateCountsAsync(allDtos);

            var lookup = allDtos.ToDictionary(c => c.Id);

            foreach (var dto in allDtos)
            {
                dto.Children = new List<CompanyDto>();
            }

            foreach (var dto in allDtos)
            {
                if (dto.ParentId.HasValue && lookup.TryGetValue(dto.ParentId.Value, out var parent))
                {
                    parent.Children.Add(dto);
                }
            }

            return allDtos.ToList();
        }

        private async Task PopulateCountsAsync(List<CompanyDto> dtos)
        {
            var ids = dtos.Select(d => d.Id).ToList();
            var partnersCounts = await UnitOfWork.Companies.GetPartnersCountAsync(ids);

            foreach (var dto in dtos)
            {
                dto.PartnersCount = partnersCounts.TryGetValue(dto.Id, out var pc) ? pc : 0;
            }
        }

        protected override async Task ValidateCreateAsync(CreateCompanyDto dto)
        {
            if (dto.ParentId.HasValue)
            {
                var parent = await UnitOfWork.Companies.GetByIdAsync(dto.ParentId.Value);
                if (parent == null)
                {
                    throw new InvalidOperationException(Messages.ParentCompanyNotFound);
                }
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyDto dto, Company entity)
        {
            entity.StoryEn = dto.StoryEn;
            entity.StoryAr = dto.StoryAr;
            entity.MissionEn = dto.MissionEn;
            entity.MissionAr = dto.MissionAr;
            entity.VisionEn = dto.VisionEn;
            entity.VisionAr = dto.VisionAr;
            entity.DescriptionAr = dto.DescriptionAr;
            entity.DescriptionEn = dto.DescriptionEn;

            // Check if deactivating a company with active children
            if (dto.IsActive == false && entity.IsActive)
            {
                if (await UnitOfWork.Companies.HasActiveChildrenAsync(id))
                {
                    throw new InvalidOperationException(Messages.CompanyHasActiveChildren);
                }
            }

            if (dto.ParentId.HasValue)
            {
                if (dto.ParentId.Value == id)
                {
                    throw new InvalidOperationException(Messages.CompanyCannotBeOwnParent);
                }

                var parent = await UnitOfWork.Companies.GetByIdAsync(dto.ParentId.Value);
                if (parent == null)
                {
                    throw new InvalidOperationException(Messages.ParentCompanyNotFound);
                }

                // Check for circular reference: walk up the parent chain
                var currentParent = parent;
                while (currentParent.ParentId.HasValue)
                {
                    if (currentParent.ParentId.Value == id)
                    {
                        throw new InvalidOperationException(Messages.CompanyCircularReference);
                    }
                    currentParent = await UnitOfWork.Companies.GetByIdAsync(currentParent.ParentId.Value);
                    if (currentParent == null) break;
                }
            }
        }

        public async Task<string> UploadLogoAsync(Guid id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new InvalidOperationException("No file uploaded.");
            }

            var company = await UnitOfWork.Companies.GetByIdAsync(id)
                ?? throw new InvalidOperationException(Messages.CompanyNotFound);

            if (!string.IsNullOrEmpty(company.Logo))
            {
                var oldAbsolutePath = Path.Combine(_webHostEnvironment.WebRootPath, company.Logo);
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

            company.Logo = relativePath;
            await UnitOfWork.Companies.UpdateAsync(company);
            await UnitOfWork.SaveChangesAsync();

            return UrlHelper.BuildFullUrl(relativePath, _httpContextAccessor) ?? relativePath;
        }

        protected override async Task ValidateDeleteAsync(Guid id, Company entity)
        {
            // Check for child companies
            if (await UnitOfWork.Companies.HasChildrenAsync(id))
            {
                throw new InvalidOperationException(Messages.CompanyHasChildren);
            }

            // Check for employees linked to this company
            var employees = await UnitOfWork.Employees.GetByCompanyIdAsync(id);
            if (employees.Any())
            {
                throw new InvalidOperationException(Messages.CompanyHasEmployees);
            }
        }
    }
}
