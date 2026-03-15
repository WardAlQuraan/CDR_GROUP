using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<Position?> GetByNameAsync(string name);
        Task<IEnumerable<Position>> GetActivePositionsAsync();
        Task<(IEnumerable<Position> Items, int TotalCount)> GetPositionsPagedAsync(PagedRequest request);
        Task<bool> HasEmployeesAsync(Guid positionId);
        Task<int> GetEmployeeCountAsync(Guid positionId);
    }
}
