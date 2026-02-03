using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Persistence.Data;

namespace cdr_group.Persistence.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _dbSet
                .FirstOrDefaultAsync(r => r.Name == name && !r.IsDeleted);
        }

        public async Task<Role?> GetWithPermissionsAsync(Guid id)
        {
            return await _dbSet
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<Role?> GetWithAllPermissionsAsync(Guid id)
        {
            return await _dbSet
                .Include(r => r.RolePermissions.Where(rp => true))
                    .ThenInclude(rp => rp.Permission)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<IEnumerable<Role>> GetRolesWithPermissionsAsync()
        {
            return await _dbSet
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Where(r => !r.IsDeleted)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Role> Roles, int TotalCount)> GetRolesWithPermissionsPagedAsync(PagedRequest request)
        {
            var query = _dbSet
                .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                .Where(r => !r.IsDeleted);

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(r =>
                    r.Name.ToLower().Contains(searchTerm) ||
                    (r.Description != null && r.Description.ToLower().Contains(searchTerm)));
            }

            // Apply sorting
            query = request.SortBy?.ToLower() switch
            {
                "name" => request.SortDescending ? query.OrderByDescending(r => r.Name) : query.OrderBy(r => r.Name),
                "description" => request.SortDescending ? query.OrderByDescending(r => r.Description) : query.OrderBy(r => r.Description),
                "createdat" => request.SortDescending ? query.OrderByDescending(r => r.CreatedAt) : query.OrderBy(r => r.CreatedAt),
                _ => request.SortDescending ? query.OrderByDescending(r => r.CreatedAt) : query.OrderBy(r => r.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var roles = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (roles, totalCount);
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _dbSet.AnyAsync(r => r.Name == name && !r.IsDeleted);
        }

        public async Task<List<RolePermission>> GetAllRolePermissionsAsync(Guid roleId)
        {
            return await _context.RolePermissions
                .IgnoreQueryFilters()
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync();
        }

        public async Task AddRolePermissionAsync(RolePermission rolePermission)
        {
            await _context.RolePermissions.AddAsync(rolePermission);
        }

        public async Task UpdateRolePermissionAsync(RolePermission rolePermission)
        {
            _context.RolePermissions.Update(rolePermission);
            await Task.CompletedTask;
        }
    }
}
