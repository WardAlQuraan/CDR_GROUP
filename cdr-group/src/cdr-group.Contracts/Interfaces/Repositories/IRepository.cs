using cdr_group.Contracts.DTOs.Common;
using cdr_group.Domain.Entities.Base;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(PagedRequest request);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(Guid id);
    }
}
