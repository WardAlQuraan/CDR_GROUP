using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Employee?> GetWithManagerAsync(Guid id)
        {
            return await _dbSet
                .Include(e => e.Manager)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<Employee?> GetWithSubordinatesAsync(Guid id)
        {
            return await _dbSet
                .Include(e => e.Manager)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.Subordinates.Where(s => !s.IsDeleted))
                    .ThenInclude(s => s.Department)
                .Include(e => e.Subordinates.Where(s => !s.IsDeleted))
                    .ThenInclude(s => s.Position)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<Employee?> GetByEmployeeCodeAsync(string employeeCode)
        {
            return await _dbSet
                .Include(e => e.Manager)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.EmployeeCode == employeeCode && !e.IsDeleted);
        }

        public async Task<Employee?> GetByUserIdAsync(Guid userId, Guid? execludedId = null)
        {
            return await _dbSet
                .Include(e => e.Manager)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.UserId == userId && (!execludedId.HasValue || e.Id != execludedId) && !e.IsDeleted);
        }

        public async Task<IEnumerable<Employee>> GetByManagerIdAsync(Guid managerId)
        {
            return await _dbSet
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Where(e => e.ManagerId == managerId && !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(Guid departmentId)
        {
            return await _dbSet
                .Include(e => e.Manager)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Where(e => e.DepartmentId == departmentId && !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Employee> Items, int TotalCount)> GetEmployeesPagedAsync(PagedRequest request)
        {
            var query = _dbSet
                .Include(e => e.Manager)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Where(e => !e.IsDeleted);

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, e => e.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }

        public async Task<bool> EmployeeCodeExistsAsync(string employeeCode, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(e =>
                e.EmployeeCode == employeeCode &&
                !e.IsDeleted &&
                (excludeId == null || e.Id != excludeId));
        }
    }
}
