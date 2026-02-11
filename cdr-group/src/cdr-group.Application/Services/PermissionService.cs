using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Identity;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class PermissionService : BaseService<Permission, PermissionDto, CreatePermissionDto, UpdatePermissionDto>, IPermissionService
    {
        public PermissionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<Permission> Repository => UnitOfWork.Permissions;

        public override async Task<PagedResult<PermissionDto>> GetPagedAsync(PagedRequest request)
        {
            var (permissions, totalCount) = await UnitOfWork.Permissions.GetPermissionsPagedAsync(request);
            var permissionDtos = Mapper.Map<List<PermissionDto>>(permissions);
            return new PagedResult<PermissionDto>(permissionDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public async Task<PermissionDto?> GetByNameAsync(string name)
        {
            var permission = await UnitOfWork.Permissions.GetByNameAsync(name);
            return Mapper.Map<PermissionDto>(permission);
        }

        public async Task<IEnumerable<PermissionDto>> GetByModuleAsync(string module)
        {
            var permissions = await UnitOfWork.Permissions.GetByModuleAsync(module);
            return Mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        public async Task<IEnumerable<PermissionDto>> GetByRoleIdAsync(Guid roleId)
        {
            var permissions = await UnitOfWork.Permissions.GetPermissionsByRoleIdAsync(roleId);
            return Mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        public async Task<IEnumerable<PermissionDto>> GetByUserIdAsync(Guid userId)
        {
            var permissions = await UnitOfWork.Permissions.GetPermissionsByUserIdAsync(userId);
            return Mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        protected override async Task ValidateCreateAsync(CreatePermissionDto dto)
        {
            if (await UnitOfWork.Permissions.NameExistsAsync(dto.Name))
            {
                throw new InvalidOperationException(Messages.PermissionNameExists);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdatePermissionDto dto, Permission entity)
        {
            if (dto.Name != null && dto.Name != entity.Name)
            {
                if (await UnitOfWork.Permissions.NameExistsAsync(dto.Name))
                {
                    throw new InvalidOperationException(Messages.PermissionNameExists);
                }
            }
        }
    }
}
