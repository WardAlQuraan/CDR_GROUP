using cdr_group.Contracts.DTOs.SalaryHistory;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface ISalaryHistoryService : IBaseService<SalaryHistoryDto, CreateSalaryHistoryDto, UpdateSalaryHistoryDto>
    {
        Task<IEnumerable<SalaryHistoryDto>> GetByEmployeeIdAsync(Guid employeeId);
    }
}
