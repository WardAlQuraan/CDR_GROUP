using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Persistence.Data;

namespace cdr_group.Persistence.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Permission?> GetByNameAsync(string name)
        {
            return await _dbSet
                .FirstOrDefaultAsync(p => p.Name == name && !p.IsDeleted);
        }

        public async Task<IEnumerable<Permission>> GetByModuleAsync(string module)
        {
            return await _dbSet
                .Where(p => p.Module == module && !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(Guid roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId && !rp.IsDeleted)
                .Include(rp => rp.Permission)
                .Select(rp => rp.Permission)
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByUserIdAsync(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId && !ur.IsDeleted)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Where(rp => !rp.IsDeleted)
                .Select(rp => rp.Permission)
                .Where(p => !p.IsDeleted)
                .Distinct()
                .ToListAsync();
        }

        public async Task<(IEnumerable<Permission> Permissions, int TotalCount)> GetPermissionsPagedAsync(PagedRequest request)
        {
            var query = _dbSet.Where(p => !p.IsDeleted);

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(searchTerm) ||
                    (p.Description != null && p.Description.ToLower().Contains(searchTerm)) ||
                    (p.Module != null && p.Module.ToLower().Contains(searchTerm)));
            }

            // Apply sorting
            query = request.SortBy?.ToLower() switch
            {
                "name" => request.SortDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "description" => request.SortDescending ? query.OrderByDescending(p => p.Description) : query.OrderBy(p => p.Description),
                "module" => request.SortDescending ? query.OrderByDescending(p => p.Module) : query.OrderBy(p => p.Module),
                "createdat" => request.SortDescending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
                _ => request.SortDescending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var permissions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (permissions, totalCount);
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _dbSet.AnyAsync(p => p.Name == name && !p.IsDeleted);
        }
    }
}
