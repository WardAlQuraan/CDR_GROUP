using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities.Base;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
        }

        public virtual async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(PagedRequest request)
        {
            var query = _dbSet.Where(e => !e.IsDeleted);

            // Apply search, sort, and paging
            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await UpdateAsync(entity);
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id && !e.IsDeleted);
        }
    }
}
