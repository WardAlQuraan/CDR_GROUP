using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using cdr_group.Application.Helpers;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Employee;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

namespace cdr_group.Application.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeDto, CreateEmployeeDto, UpdateEmployeeDto>, IEmployeeService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override IRepository<Employee> Repository => UnitOfWork.Employees;

        public override async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var (employees, _) = await UnitOfWork.Employees.GetEmployeesPagedAsync(new PagedRequest { PageSize = int.MaxValue });
            var employeeDtos = Mapper.Map<List<EmployeeDto>>(employees);
            await PopulateFilePathsAsync(employeeDtos);
            return employeeDtos;
        }

        public override async Task<PagedResult<EmployeeDto>> GetPagedAsync(PagedRequest request)
        {
            var (employees, totalCount) = await UnitOfWork.Employees.GetEmployeesPagedAsync(request);
            var employeeDtos = Mapper.Map<List<EmployeeDto>>(employees);
            await PopulateFilePathsAsync(employeeDtos);
            return new PagedResult<EmployeeDto>(employeeDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<EmployeeDto?> GetByIdAsync(Guid id)
        {
            var employee = await UnitOfWork.Employees.GetWithManagerAsync(id);
            var dto = Mapper.Map<EmployeeDto>(employee);
            if (dto != null)
            {
                await PopulateFilePathAsync(dto);
            }
            return dto;
        }

        public async Task<EmployeeDto?> GetByEmployeeCodeAsync(string employeeCode)
        {
            var employee = await UnitOfWork.Employees.GetByEmployeeCodeAsync(employeeCode);
            var dto = Mapper.Map<EmployeeDto>(employee);
            if (dto != null)
            {
                await PopulateFilePathAsync(dto);
            }
            return dto;
        }

        public async Task<EmployeeDto?> GetByUserIdAsync(Guid userId)
        {
            var employee = await UnitOfWork.Employees.GetByUserIdAsync(userId);
            var dto = Mapper.Map<EmployeeDto>(employee);
            if (dto != null)
            {
                await PopulateFilePathAsync(dto);
            }
            return dto;
        }

        public async Task<EmployeeWithSubordinatesDto?> GetWithSubordinatesAsync(Guid id)
        {
            var employee = await UnitOfWork.Employees.GetWithSubordinatesAsync(id);
            var dto = Mapper.Map<EmployeeWithSubordinatesDto>(employee);
            if (dto != null)
            {
                await PopulateFilePathAsync(dto);
            }
            return dto;
        }

        public async Task<IEnumerable<EmployeeBasicDto>> GetSubordinatesAsync(Guid managerId)
        {
            var subordinates = await UnitOfWork.Employees.GetByManagerIdAsync(managerId);
            return Mapper.Map<IEnumerable<EmployeeBasicDto>>(subordinates);
        }

        public async Task<IEnumerable<EmployeeDto>> GetByCompanyIdAsync(Guid? companyId)
        {
            var employees = await UnitOfWork.Employees.GetByCompanyIdAsync(companyId);
            var employeeDtos = Mapper.Map<List<EmployeeDto>>(employees);
            await PopulateFilePathsAsync(employeeDtos);
            return employeeDtos;
        }

        public async Task<IEnumerable<EmployeeTreeNodeDto>> GetTreeAsync(GetTreeRequest request)
        {

            IEnumerable<Employee> employees =  new List<Employee>();

            if (request.CompanyId.HasValue)
            {
                employees = await UnitOfWork.Employees.GetByCompanyIdAsync(request.CompanyId);
            }else if (!string.IsNullOrEmpty(request.CompanyCode))
            {
                employees = await UnitOfWork.Employees.GetByCompanyCodeAsync(request.CompanyCode);
            }
            else
            {
                employees = await UnitOfWork.Employees.GetAllAsync();
            }


                // Create a dictionary for quick lookup
                var employeeDict = employees.ToDictionary(e => e.Id, e => new EmployeeTreeNodeDto
                {
                    Id = e.Id,
                    EmployeeCode = e.EmployeeCode,
                    FirstNameEn = e.FirstNameEn,
                    LastNameEn = e.LastNameEn,
                    FirstNameAr = e.FirstNameAr,
                    LastNameAr = e.LastNameAr,
                    Email = e.Email,
                    Phone = e.Phone,
                    HireDate = e.HireDate,
                    Salary = e.Salary,
                    PositionId = e.PositionId,
                    PositionNameEn = e.Position?.NameEn,
                    PositionNameAr = e.Position?.NameAr,
                    CompanyId = e.CompanyId,
                    CompanyNameEn = e.Company?.NameEn,
                    CompanyNameAr = e.Company?.NameAr,
                    ManagerId = e.ManagerId,
                    UserId = e.UserId,
                    Username = e.User?.Username,
                    IsActive = e.IsActive,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                });

            // Populate file paths for all employees
            foreach (var employee in employees)
            {
                var files = await UnitOfWork.FileAttachments.GetByEntityAsync(employee.Id, EntityTypes.Employee);
                var file = files.FirstOrDefault();
                employeeDict[employee.Id].FilePath = UrlHelper.BuildFullUrl(file?.Path, _httpContextAccessor);
            }

            // Build tree structure
            var rootNodes = new List<EmployeeTreeNodeDto>();

            foreach (var employee in employees)
            {
                var node = employeeDict[employee.Id];

                if (employee.ManagerId.HasValue && employeeDict.ContainsKey(employee.ManagerId.Value))
                {
                    // Add as child to manager
                    employeeDict[employee.ManagerId.Value].Children.Add(node);
                }
                else
                {
                    // No manager or manager not in list - this is a root node
                    rootNodes.Add(node);
                }
            }

            return rootNodes;
        }

        public async Task<EmployeeDto?> AssignManagerAsync(Guid employeeId, Guid? managerId)
        {
            var employee = await UnitOfWork.Employees.GetByIdAsync(employeeId);
            if (employee == null) return null;

            // Prevent self-reference
            if (managerId == employeeId)
            {
                throw new InvalidOperationException(Messages.EmployeeCannotBeOwnManager);
            }

            // Prevent circular reference
            if (managerId.HasValue)
            {
                if (await IsCircularReference(employeeId, managerId.Value))
                {
                    throw new InvalidOperationException(Messages.EmployeeCircularReference);
                }

                var manager = await UnitOfWork.Employees.GetByIdAsync(managerId.Value);
                if (manager == null)
                {
                    throw new InvalidOperationException(Messages.ManagerNotFound);
                }
            }

            employee.ManagerId = managerId;
            await UnitOfWork.Employees.UpdateAsync(employee);
            await UnitOfWork.SaveChangesAsync();

            return await GetByIdAsync(employeeId);
        }

        public async Task<EmployeeDto?> LinkToUserAsync(Guid employeeId, Guid? userId)
        {
            var employee = await UnitOfWork.Employees.GetByIdAsync(employeeId);
            if (employee == null) return null;

            if (userId.HasValue)
            {
                var user = await UnitOfWork.Users.GetByIdAsync(userId.Value);
                if (user == null)
                {
                    throw new InvalidOperationException(Messages.UserNotFound);
                }

                // Check if user is already linked to another employee
                var existingEmployee = await UnitOfWork.Employees.GetByUserIdAsync(userId.Value);
                if (existingEmployee != null && existingEmployee.Id != employeeId)
                {
                    throw new InvalidOperationException(Messages.UserAlreadyLinked);
                }
            }

            employee.UserId = userId;
            await UnitOfWork.Employees.UpdateAsync(employee);
            await UnitOfWork.SaveChangesAsync();

            return await GetByIdAsync(employeeId);
        }

        protected override async Task ValidateCreateAsync(CreateEmployeeDto dto)
        {
            await ValidateEmployeeCode(dto.EmployeeCode);

            if (dto.ManagerId.HasValue)
            {
                var manager = await UnitOfWork.Employees.GetByIdAsync(dto.ManagerId.Value);
                if (manager == null)
                {
                    throw new InvalidOperationException(Messages.ManagerNotFound);
                }
            }

            await ValidateUser(dto.UserId, null, null);
            await ValidateCompany(dto.CompanyId);
            await ValidatePosition(dto.PositionId, dto.Salary);
        }



        public override async Task<EmployeeDto?> UpdateAsync(Guid id, UpdateEmployeeDto dto)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null) return default;

            await ValidateUpdateAsync(id, dto, entity);

            await UnitOfWork.BeginTransactionAsync();
            try
            {
                if (dto.Salary.HasValue && dto.Salary != entity.Salary)
                {
                    var salaryHistory = new SalaryHistory
                    {
                        Id = Guid.NewGuid(),
                        EmployeeId = entity.Id,
                        OldSalary = entity.Salary,
                        NewSalary = dto.Salary.Value,
                        EffectiveDate = DateTime.UtcNow,
                        Reason = dto.SalaryChangeReason
                    };

                    await UnitOfWork.SalaryHistories.AddAsync(salaryHistory);
                }
                Mapper.Map(dto, entity);

                await BeforeUpdateAsync(entity, dto);

                await Repository.UpdateAsync(entity);
                await UnitOfWork.SaveChangesAsync();

                await AfterUpdateAsync(entity, dto);

                await UnitOfWork.CommitTransactionAsync();

                return await GetByIdAsync(id);
            }
            catch
            {
                await UnitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
        protected override async Task BeforeUpdateAsync(Employee entity, UpdateEmployeeDto dto)
        {
            // Auto-track salary changes
            await base.BeforeUpdateAsync(entity, dto);
        }


        protected override async Task ValidateUpdateAsync(Guid id, UpdateEmployeeDto dto, Employee entity)
        {
            if (dto.EmployeeCode != null && dto.EmployeeCode != entity.EmployeeCode)
            {
                await ValidateEmployeeCode(dto.EmployeeCode);
            }

            if (dto.ManagerId.HasValue)
            {
                if (dto.ManagerId == id)
                {
                    throw new InvalidOperationException(Messages.EmployeeCannotBeOwnManager);
                }

                if (await IsCircularReference(id, dto.ManagerId.Value))
                {
                    throw new InvalidOperationException(Messages.EmployeeCircularReference);
                }

                var manager = await UnitOfWork.Employees.GetByIdAsync(dto.ManagerId.Value);
                if (manager == null)
                {
                    throw new InvalidOperationException(Messages.ManagerNotFound);
                }
            }

            await ValidateUser(dto.UserId, entity.UserId, id);
            await ValidateCompany(dto.CompanyId);
            await ValidatePosition(dto.PositionId, dto.Salary);
        }

        private async Task ValidateCompany(Guid? companyId)
        {
            if (companyId.HasValue)
            {
                var company = await UnitOfWork.Companies.GetByIdAsync(companyId.Value);
                if (company == null)
                {
                    throw new InvalidOperationException(Messages.CompanyNotFound);
                }
            }
        }
        private async Task ValidateEmployeeCode(string code)
        {
            if (await UnitOfWork.Employees.EmployeeCodeExistsAsync(code))
            {
                throw new InvalidOperationException(Messages.EmployeeCodeExists);
            }
        }

        private async Task ValidateUser(Guid? newUserId, Guid? oldUserId, Guid? execludedId)
        {
            if (newUserId.HasValue && newUserId != oldUserId)
            {
                var user = await UnitOfWork.Users.GetByIdAsync(newUserId.Value);
                if (user == null)
                {
                    throw new InvalidOperationException(Messages.UserNotFound);
                }

                var existingEmployee = await UnitOfWork.Employees.GetByUserIdAsync(newUserId.Value, execludedId);
                if (existingEmployee != null)
                {
                    throw new InvalidOperationException(Messages.UserAlreadyLinked);
                }
            }
        }

        private async Task ValidatePosition(Guid? positionId, decimal? salary)
        {
            if (positionId.HasValue)
            {
                var position = await UnitOfWork.Positions.GetByIdAsync(positionId.Value);
                if(position is null)
                {
                    throw new InvalidOperationException(Messages.PositionNotFound);
                }
                if (salary.HasValue)
                {
                    if (position.MinSalary.HasValue && salary < position.MinSalary)
                    {
                        throw new InvalidOperationException(Messages.SalaryBelowMinimum);
                    }
                    if (position.MaxSalary.HasValue && salary > position.MaxSalary)
                    {
                        throw new InvalidOperationException(Messages.SalaryAboveMaximum);
                    }
                }
            }
        }


        private async Task<bool> IsCircularReference(Guid employeeId, Guid potentialManagerId)
        {
            var visited = new HashSet<Guid> { employeeId };
            var currentId = potentialManagerId;

            while (currentId != Guid.Empty)
            {
                if (visited.Contains(currentId))
                {
                    return true;
                }

                visited.Add(currentId);

                var employee = await UnitOfWork.Employees.GetByIdAsync(currentId);
                if (employee?.ManagerId == null)
                {
                    break;
                }

                currentId = employee.ManagerId.Value;
            }

            return false;
        }

        protected override async Task BeforeDeleteAsync(Employee entity)
        {
            // Delete all files associated with this employee
            var files = await UnitOfWork.FileAttachments.GetByEntityAsync(entity.Id, EntityTypes.Employee);

            foreach (var file in files)
            {
                // Delete physical file from wwwroot
                var absolutePath = Path.Combine(_webHostEnvironment.WebRootPath, file.Path);
                if (File.Exists(absolutePath))
                {
                    File.Delete(absolutePath);
                }

                // Soft delete the file record
                await UnitOfWork.FileAttachments.DeleteAsync(file);
            }

            await base.BeforeDeleteAsync(entity);
        }

        private async Task PopulateFilePathAsync(EmployeeDto dto)
        {
            var files = await UnitOfWork.FileAttachments.GetByEntityAsync(dto.Id, EntityTypes.Employee);
            var file = files.FirstOrDefault();
            dto.FilePath = UrlHelper.BuildFullUrl(file?.Path, _httpContextAccessor);
        }

        private async Task PopulateFilePathsAsync(List<EmployeeDto> dtos)
        {
            foreach (var dto in dtos)
            {
                var files = await UnitOfWork.FileAttachments.GetByEntityAsync(dto.Id, EntityTypes.Employee);
                var file = files.FirstOrDefault();
                dto.FilePath = UrlHelper.BuildFullUrl(file?.Path, _httpContextAccessor);
            }
        }
    }
}
