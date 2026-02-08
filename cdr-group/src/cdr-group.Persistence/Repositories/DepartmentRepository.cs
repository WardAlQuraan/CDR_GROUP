using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;
using cdr_group.Persistence.Helpers;

namespace cdr_group.Persistence.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Department?> GetWithParentAsync(Guid id)
        {
            return await _dbSet
                .Include(d => d.ParentDepartment)
                .Include(d => d.Manager)
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<Department?> GetWithSubDepartmentsAsync(Guid id)
        {
            return await _dbSet
                .Include(d => d.ParentDepartment)
                .Include(d => d.Manager)
                .Include(d => d.SubDepartments.Where(s => !s.IsDeleted))
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<Department?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .Include(d => d.ParentDepartment)
                .Include(d => d.Manager)
                .FirstOrDefaultAsync(d => d.Code == code && !d.IsDeleted);
        }

        public async Task<Department?> GetByNameAsync(string name)
        {
            return await _dbSet
                .Include(d => d.ParentDepartment)
                .Include(d => d.Manager)
                .FirstOrDefaultAsync(d => (d.NameEn == name || d.NameAr == name) && !d.IsDeleted);
        }

        public async Task<IEnumerable<Department>> GetByParentIdAsync(Guid parentId)
        {
            return await _dbSet
                .Include(d => d.Manager)
                .Where(d => d.ParentDepartmentId == parentId && !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetRootDepartmentsAsync()
        {
            return await _dbSet
                .Include(d => d.Manager)
                .Where(d => d.ParentDepartmentId == null && !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetActiveDepartmentsAsync()
        {
            return await _dbSet
                .Include(d => d.ParentDepartment)
                .Include(d => d.Manager)
                .Where(d => d.IsActive && !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Department> Items, int TotalCount)> GetDepartmentsPagedAsync(PagedRequest request)
        {
            var query = _dbSet
                .Include(d => d.ParentDepartment)
                .Include(d => d.Manager)
                .Include(d=>d.Company)
                .Where(d => !d.IsDeleted);

            query = QueryHelper.ApplySearch(query, request);
            query = QueryHelper.ApplySort(query, request, d => d.CreatedAt);

            var totalCount = await query.CountAsync();
            var items = await QueryHelper.ApplyPaging(query, request).ToListAsync();

            return (items, totalCount);
        }

        public async Task<bool> DepartmentCodeExistsAsync(string code, Guid? excludeId = null)
        {
            return await _dbSet.AnyAsync(d =>
                d.Code == code &&
                !d.IsDeleted &&
                (excludeId == null || d.Id != excludeId));
        }

        public async Task<bool> HasEmployeesAsync(Guid departmentId)
        {
            return await _context.Employees.AnyAsync(e =>
                e.DepartmentId == departmentId && !e.IsDeleted);
        }

        public async Task<bool> HasSubDepartmentsAsync(Guid departmentId)
        {
            return await _dbSet.AnyAsync(d =>
                d.ParentDepartmentId == departmentId && !d.IsDeleted);
        }
    }
}
