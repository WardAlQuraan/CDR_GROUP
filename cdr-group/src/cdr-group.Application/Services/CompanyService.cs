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
            return Mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        public override async Task<PagedResult<CompanyDto>> GetPagedAsync(PagedRequest request)
        {
            var (companies, totalCount) = await UnitOfWork.Companies.GetCompaniesPagedAsync(request);
            var companyDtos = Mapper.Map<List<CompanyDto>>(companies);
            return new PagedResult<CompanyDto>(companyDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<CompanyDto?> GetByCodeAsync(string code)
        {
            var company = await UnitOfWork.Companies.GetByCodeAsync(code);
            return Mapper.Map<CompanyDto>(company);
        }

        public async Task<IEnumerable<CompanyDto>> GetActiveCompaniesAsync()
        {
            var companies = await UnitOfWork.Companies.GetActiveCompaniesAsync();
            return Mapper.Map<IEnumerable<CompanyDto>>(companies);
        }

        protected override async Task ValidateCreateAsync(CreateCompanyDto dto)
        {
            if (await UnitOfWork.Companies.CompanyCodeExistsAsync(dto.Code))
            {
                throw new InvalidOperationException(Messages.CompanyCodeExists);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateCompanyDto dto, Company entity)
        {
            if (dto.Code != null && dto.Code != entity.Code)
            {
                if (await UnitOfWork.Companies.CompanyCodeExistsAsync(dto.Code, id))
                {
                    throw new InvalidOperationException(Messages.CompanyCodeExists);
                }
            }
        }

        protected override async Task ValidateDeleteAsync(Guid id, Company entity)
        {
            // Check for employees linked to this company
            var employees = await UnitOfWork.Employees.GetByCompanyIdAsync(id);
            if (employees.Any())
            {
                throw new InvalidOperationException(Messages.CompanyHasEmployees);
            }
        }
    }
}
