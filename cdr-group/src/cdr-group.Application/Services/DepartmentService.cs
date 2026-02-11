using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Department;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

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
                await ValidateEntityExistsAsync(UnitOfWork.Employees, managerId.Value, "Manager");
            }

            department.ManagerId = managerId;
            await UnitOfWork.Departments.UpdateAsync(department);
            await UnitOfWork.SaveChangesAsync();

            return await GetByIdAsync(departmentId);
        }

        protected override async Task ValidateCreateAsync(CreateDepartmentDto dto)
        {
            await ValidateEntityExistsAsync(UnitOfWork.Companies, dto.CompanyId, "Company");
            await ValidateCodeUniqueAsync(dto.Code, dto.CompanyId);
            await ValidateParentDepartmentAsync(dto.ParentDepartmentId, null, dto.CompanyId);
            await ValidateManagerAsync(dto.ManagerId);
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateDepartmentDto dto, Department entity)
        {
            if (dto.CompanyId.HasValue)
            {
                await ValidateEntityExistsAsync(UnitOfWork.Companies, dto.CompanyId.Value, "Company");
            }

            var effectiveCompanyId = dto.CompanyId ?? entity.CompanyId;

            if (dto.Code != null && dto.Code != entity.Code)
            {
                await ValidateCodeUniqueAsync(dto.Code, effectiveCompanyId, id);
            }

            await ValidateParentDepartmentAsync(dto.ParentDepartmentId, id, effectiveCompanyId);
            await ValidateManagerAsync(dto.ManagerId);
        }

        protected override async Task ValidateDeleteAsync(Guid id, Department entity)
        {
            if (await UnitOfWork.Departments.HasEmployeesAsync(id))
            {
                throw new InvalidOperationException(Messages.DepartmentHasEmployees);
            }

            if (await UnitOfWork.Departments.HasSubDepartmentsAsync(id))
            {
                throw new InvalidOperationException(Messages.DepartmentHasSubDepartments);
            }
        }

        private async Task ValidateEntityExistsAsync<T>(IRepository<T> repository, Guid id, string entityName)
            where T : Domain.Entities.Base.BaseEntity
        {
            if (!await repository.ExistsAsync(id))
            {
                throw new InvalidOperationException(entityName == "Company" ? Messages.CompanyNotFound : Messages.ManagerNotFound);
            }
        }

        private async Task ValidateCodeUniqueAsync(string code, Guid companyId, Guid? excludeId = null)
        {
            if (await UnitOfWork.Departments.DepartmentCodeExistsAsync(code, companyId, excludeId))
            {
                throw new InvalidOperationException(Messages.DepartmentCodeExists);
            }
        }

        private async Task ValidateParentDepartmentAsync(Guid? parentDepartmentId, Guid? currentDepartmentId, Guid companyId)
        {
            if (!parentDepartmentId.HasValue) return;

            if (parentDepartmentId == currentDepartmentId)
            {
                throw new InvalidOperationException(Messages.DepartmentCannotBeOwnParent);
            }

            var parentDepartment = await UnitOfWork.Departments.GetByIdAsync(parentDepartmentId.Value);
            if (parentDepartment == null)
            {
                throw new InvalidOperationException(Messages.ParentDepartmentNotFound);
            }

            if (parentDepartment.CompanyId != companyId)
            {
                throw new InvalidOperationException(Messages.ParentDepartmentDifferentCompany);
            }

            if (currentDepartmentId.HasValue && await IsCircularReference(currentDepartmentId.Value, parentDepartmentId.Value))
            {
                throw new InvalidOperationException(Messages.DepartmentCircularReference);
            }
        }

        private async Task ValidateManagerAsync(Guid? managerId)
        {
            if (managerId.HasValue)
            {
                await ValidateEntityExistsAsync(UnitOfWork.Employees, managerId.Value, "Manager");
            }
        }

        private async Task<bool> IsCircularReference(Guid departmentId, Guid potentialParentId)
        {
            var visited = new HashSet<Guid> { departmentId };
            var currentId = potentialParentId;

            while (currentId != Guid.Empty)
            {
                if (!visited.Add(currentId))
                {
                    return true;
                }

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
