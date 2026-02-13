using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.SalaryHistory;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class SalaryHistoryService : BaseService<SalaryHistory, SalaryHistoryDto, CreateSalaryHistoryDto, UpdateSalaryHistoryDto>, ISalaryHistoryService
    {
        public SalaryHistoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<SalaryHistory> Repository => UnitOfWork.SalaryHistories;

        public override async Task<SalaryHistoryDto?> GetByIdAsync(Guid id)
        {
            var entity = await UnitOfWork.SalaryHistories.GetWithEmployeeAsync(id);
            return Mapper.Map<SalaryHistoryDto>(entity);
        }

        public override async Task<IEnumerable<SalaryHistoryDto>> GetAllAsync()
        {
            var (items, _) = await UnitOfWork.SalaryHistories.GetSalaryHistoriesPagedAsync(new PagedRequest { PageSize = int.MaxValue });
            return Mapper.Map<List<SalaryHistoryDto>>(items);
        }

        public override async Task<PagedResult<SalaryHistoryDto>> GetPagedAsync(PagedRequest request)
        {
            var (items, totalCount) = await UnitOfWork.SalaryHistories.GetSalaryHistoriesPagedAsync(request);
            var dtos = Mapper.Map<List<SalaryHistoryDto>>(items);
            return new PagedResult<SalaryHistoryDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<IEnumerable<SalaryHistoryDto>> GetByEmployeeIdAsync(Guid employeeId)
        {
            var items = await UnitOfWork.SalaryHistories.GetByEmployeeIdAsync(employeeId);
            return Mapper.Map<List<SalaryHistoryDto>>(items);
        }

        protected override async Task ValidateCreateAsync(CreateSalaryHistoryDto dto)
        {
            if (!await UnitOfWork.Employees.ExistsAsync(dto.EmployeeId))
            {
                throw new InvalidOperationException(Messages.EmployeeNotFoundForSalaryHistory);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateSalaryHistoryDto dto, SalaryHistory entity)
        {
            await Task.CompletedTask;
        }
    }
}
