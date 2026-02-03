using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Department;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class DepartmentService : BaseService<Department, DepartmentDto, CreateDepartmentDto, UpdateDepartmentDto>, IDepartmentService
    {
        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<Department> Repository => UnitOfWork.Departments;

        public override async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            var (departments, _) = await UnitOfWork.Departments.GetDepartmentsPagedAsync(new PagedRequest { PageSize = int.MaxValue });
            return Mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public override async Task<PagedResult<DepartmentDto>> GetPagedAsync(PagedRequest request)
        {
            var (departments, totalCount) = await UnitOfWork.Departments.GetDepartmentsPagedAsync(request);
            var departmentDtos = Mapper.Map<List<DepartmentDto>>(departments);
            return new PagedResult<DepartmentDto>(departmentDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<DepartmentDto?> GetByIdAsync(Guid id)
        {
            var department = await UnitOfWork.Departments.GetWithParentAsync(id);
            return Mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto?> GetByCodeAsync(string code)
        {
            var department = await UnitOfWork.Departments.GetByCodeAsync(code);
            return Mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentDto?> GetByNameAsync(string name)
        {
            var department = await UnitOfWork.Departments.GetByNameAsync(name);
            return Mapper.Map<DepartmentDto>(department);
        }

        public async Task<DepartmentWithSubDepartmentsDto?> GetWithSubDepartmentsAsync(Guid id)
        {
            var department = await UnitOfWork.Departments.GetWithSubDepartmentsAsync(id);
            return Mapper.Map<DepartmentWithSubDepartmentsDto>(department);
        }

        public async Task<IEnumerable<DepartmentBasicDto>> GetSubDepartmentsAsync(Guid parentId)
        {
            var subDepartments = await UnitOfWork.Departments.GetByParentIdAsync(parentId);
            return Mapper.Map<IEnumerable<DepartmentBasicDto>>(subDepartments);
        }

        public async Task<IEnumerable<DepartmentDto>> GetRootDepartmentsAsync()
        {
            var departments = await UnitOfWork.Departments.GetRootDepartmentsAsync();
            return Mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<IEnumerable<DepartmentDto>> GetActiveDepartmentsAsync()
        {
            var departments = await UnitOfWork.Departments.GetActiveDepartmentsAsync();
            return Mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task<DepartmentDto?> AssignManagerAsync(Guid departmentId, Guid? managerId)
        {
            var department = await UnitOfWork.Departments.GetByIdAsync(departmentId);
            if (department == null) return null;

            if (managerId.HasValue)
            {
                var manager = await UnitOfWork.Employees.GetByIdAsync(managerId.Value);
                if (manager == null)
                {
                    throw new InvalidOperationException("Manager not found.");
                }
            }

            department.ManagerId = managerId;
            await UnitOfWork.Departments.UpdateAsync(department);
            await UnitOfWork.SaveChangesAsync();

            return await GetByIdAsync(departmentId);
        }

        protected override async Task ValidateCreateAsync(CreateDepartmentDto dto)
        {
            if (await UnitOfWork.Departments.DepartmentCodeExistsAsync(dto.Code))
            {
                throw new InvalidOperationException("Department code already exists.");
            }

            if (dto.ParentDepartmentId.HasValue)
            {
                var parentDepartment = await UnitOfWork.Departments.GetByIdAsync(dto.ParentDepartmentId.Value);
                if (parentDepartment == null)
                {
                    throw new InvalidOperationException("Parent department not found.");
                }
            }

            if (dto.ManagerId.HasValue)
            {
                var manager = await UnitOfWork.Employees.GetByIdAsync(dto.ManagerId.Value);
                if (manager == null)
                {
                    throw new InvalidOperationException("Manager not found.");
                }
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateDepartmentDto dto, Department entity)
        {
            if (dto.Code != null && dto.Code != entity.Code)
            {
                if (await UnitOfWork.Departments.DepartmentCodeExistsAsync(dto.Code, id))
                {
                    throw new InvalidOperationException("Department code already exists.");
                }
            }

            if (dto.ParentDepartmentId.HasValue)
            {
                if (dto.ParentDepartmentId == id)
                {
                    throw new InvalidOperationException("A department cannot be its own parent.");
                }

                if (await IsCircularReference(id, dto.ParentDepartmentId.Value))
                {
                    throw new InvalidOperationException("Cannot assign parent department: circular reference detected.");
                }

                var parentDepartment = await UnitOfWork.Departments.GetByIdAsync(dto.ParentDepartmentId.Value);
                if (parentDepartment == null)
                {
                    throw new InvalidOperationException("Parent department not found.");
                }
            }

            if (dto.ManagerId.HasValue)
            {
                var manager = await UnitOfWork.Employees.GetByIdAsync(dto.ManagerId.Value);
                if (manager == null)
                {
                    throw new InvalidOperationException("Manager not found.");
                }
            }
        }

        protected override async Task ValidateDeleteAsync(Guid id, Department entity)
        {
            if (await UnitOfWork.Departments.HasEmployeesAsync(id))
            {
                throw new InvalidOperationException("Cannot delete department with employees. Please reassign employees first.");
            }

            if (await UnitOfWork.Departments.HasSubDepartmentsAsync(id))
            {
                throw new InvalidOperationException("Cannot delete department with sub-departments. Please delete or reassign sub-departments first.");
            }
        }

        private async Task<bool> IsCircularReference(Guid departmentId, Guid potentialParentId)
        {
            var visited = new HashSet<Guid> { departmentId };
            var currentId = potentialParentId;

            while (currentId != Guid.Empty)
            {
                if (visited.Contains(currentId))
                {
                    return true;
                }

                visited.Add(currentId);

                var department = await UnitOfWork.Departments.GetByIdAsync(currentId);
                if (department?.ParentDepartmentId == null)
                {
                    break;
                }

                currentId = department.ParentDepartmentId.Value;
            }

            return false;
        }
    }
}
