using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Identity;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class RoleService : BaseService<Role, RoleDto, CreateRoleDto, UpdateRoleDto>, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<Role> Repository => UnitOfWork.Roles;

        public override async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await UnitOfWork.Roles.GetRolesWithPermissionsAsync();
            return Mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public override async Task<PagedResult<RoleDto>> GetPagedAsync(PagedRequest request)
        {
            var (roles, totalCount) = await UnitOfWork.Roles.GetRolesWithPermissionsPagedAsync(request);
            var roleDtos = Mapper.Map<List<RoleDto>>(roles);
            return new PagedResult<RoleDto>(roleDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<RoleDto?> GetByIdAsync(Guid id)
        {
            var role = await UnitOfWork.Roles.GetWithPermissionsAsync(id);
            return Mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto?> GetByNameAsync(string name)
        {
            var role = await UnitOfWork.Roles.GetByNameAsync(name);
            return Mapper.Map<RoleDto>(role);
        }

        protected override async Task ValidateCreateAsync(CreateRoleDto dto)
        {
            if (await UnitOfWork.Roles.NameExistsAsync(dto.Name))
            {
                throw new InvalidOperationException(Messages.RoleNameExists);
            }
        }

        protected override async Task AfterCreateAsync(Role entity, CreateRoleDto dto)
        {
            if (dto.PermissionIds != null && dto.PermissionIds.Any())
            {
                await AssignPermissionsToRoleAsync(entity.Id, new AssignPermissionsDto { PermissionIds = dto.PermissionIds });
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateRoleDto dto, Role entity)
        {
            if (entity.IsSystemRole)
            {
                throw new InvalidOperationException(Messages.SystemRoleCannotBeModified);
            }

            if (dto.Name != null && dto.Name != entity.Name)
            {
                if (await UnitOfWork.Roles.NameExistsAsync(dto.Name))
                {
                    throw new InvalidOperationException(Messages.RoleNameExists);
                }
            }
        }

        protected override Task ValidateDeleteAsync(Guid id, Role entity)
        {
            if (entity.IsSystemRole)
            {
                throw new InvalidOperationException(Messages.SystemRoleCannotBeDeleted);
            }
            return Task.CompletedTask;
        }

        public async Task<RoleDto?> AssignPermissionsToRoleAsync(Guid roleId, AssignPermissionsDto dto)
        {
            // First verify the role exists
            var role = await UnitOfWork.Roles.GetByIdAsync(roleId);
            if (role == null) return null;

            // Get ALL role permissions (including soft-deleted) directly
            var allRolePermissions = await UnitOfWork.Roles.GetAllRolePermissionsAsync(roleId);

            var requestedPermissionIds = dto.PermissionIds.ToHashSet();
            var currentActivePermissionIds = allRolePermissions
                .Where(rp => !rp.IsDeleted)
                .Select(rp => rp.PermissionId)
                .ToHashSet();

            // Remove permissions not in the new list (soft-delete)
            foreach (var rolePermission in allRolePermissions.Where(rp => !rp.IsDeleted))
            {
                if (!requestedPermissionIds.Contains(rolePermission.PermissionId))
                {
                    rolePermission.IsDeleted = true;
                    rolePermission.UpdatedAt = DateTime.UtcNow;
                }
            }

            // Add new permissions or reactivate soft-deleted ones
            foreach (var permissionId in requestedPermissionIds)
            {
                if (!currentActivePermissionIds.Contains(permissionId))
                {
                    // Check if there's a soft-deleted entry to reactivate
                    var existingDeleted = allRolePermissions
                        .FirstOrDefault(rp => rp.PermissionId == permissionId && rp.IsDeleted);

                    if (existingDeleted != null)
                    {
                        // Reactivate the soft-deleted entry
                        existingDeleted.IsDeleted = false;
                        existingDeleted.UpdatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        // Verify permission exists before adding
                        var permission = await UnitOfWork.Permissions.GetByIdAsync(permissionId);
                        if (permission != null)
                        {
                            await UnitOfWork.Roles.AddRolePermissionAsync(new RolePermission
                            {
                                Id = Guid.NewGuid(),
                                RoleId = roleId,
                                PermissionId = permissionId,
                                CreatedAt = DateTime.UtcNow
                            });
                        }
                    }
                }
            }

            await UnitOfWork.SaveChangesAsync();
            return await GetByIdAsync(roleId);
        }

        public async Task<RoleDto?> RemovePermissionsFromRoleAsync(Guid roleId, List<Guid> permissionIds)
        {
            var role = await UnitOfWork.Roles.GetWithPermissionsAsync(roleId);
            if (role == null) return null;

            if (role.IsSystemRole)
            {
                throw new InvalidOperationException(Messages.SystemRolePermissionsLocked);
            }

            foreach (var rolePermission in role.RolePermissions.Where(rp => permissionIds.Contains(rp.PermissionId)))
            {
                rolePermission.IsDeleted = true;
            }

            await UnitOfWork.SaveChangesAsync();
            return await GetByIdAsync(roleId);
        }
    }
}
