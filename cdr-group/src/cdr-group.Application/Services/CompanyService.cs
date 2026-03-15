using AutoMapper;
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
        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
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

        public async Task<IEnumerable<CompanyDto>> GetTreeAsync()
        {
            var companies = await UnitOfWork.Companies.GetActiveCompaniesAsync();
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

            return allDtos.Where(c => !c.ParentId.HasValue).ToList();
        }

        private async Task PopulateCountsAsync(List<CompanyDto> dtos)
        {
            var ids = dtos.Select(d => d.Id).ToList();
            var partnersCounts = await UnitOfWork.Companies.GetPartnersCountAsync(ids);
            var employeesCounts = await UnitOfWork.Companies.GetEmployeesCountAsync(ids);

            foreach (var dto in dtos)
            {
                dto.PartnersCount = partnersCounts.TryGetValue(dto.Id, out var pc) ? pc : 0;
                dto.EmployeesCount = employeesCounts.TryGetValue(dto.Id, out var ec) ? ec : 0;
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
