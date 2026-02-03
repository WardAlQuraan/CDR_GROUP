using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<Position?> GetWithDepartmentAsync(Guid id);
        Task<Position?> GetByCodeAsync(string code);
        Task<Position?> GetByNameAsync(string name);
        Task<IEnumerable<Position>> GetByDepartmentIdAsync(Guid departmentId);
        Task<IEnumerable<Position>> GetActivePositionsAsync();
        Task<(IEnumerable<Position> Items, int TotalCount)> GetPositionsPagedAsync(PagedRequest request);
        Task<bool> PositionCodeExistsAsync(string code, Guid? excludeId = null);
        Task<bool> HasEmployeesAsync(Guid positionId);
        Task<int> GetEmployeeCountAsync(Guid positionId);
    }
}
