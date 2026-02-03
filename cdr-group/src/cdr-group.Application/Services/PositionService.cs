using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Position;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class PositionService : BaseService<Position, PositionDto, CreatePositionDto, UpdatePositionDto>, IPositionService
    {
        public PositionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<Position> Repository => UnitOfWork.Positions;

        public override async Task<IEnumerable<PositionDto>> GetAllAsync()
        {
            var (positions, _) = await UnitOfWork.Positions.GetPositionsPagedAsync(new PagedRequest { PageSize = int.MaxValue });
            return Mapper.Map<IEnumerable<PositionDto>>(positions);
        }

        public override async Task<PagedResult<PositionDto>> GetPagedAsync(PagedRequest request)
        {
            var (positions, totalCount) = await UnitOfWork.Positions.GetPositionsPagedAsync(request);
            var positionDtos = Mapper.Map<List<PositionDto>>(positions);
            return new PagedResult<PositionDto>(positionDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<PositionDto?> GetByIdAsync(Guid id)
        {
            var position = await UnitOfWork.Positions.GetWithDepartmentAsync(id);
            return Mapper.Map<PositionDto>(position);
        }

        public async Task<PositionDto?> GetByCodeAsync(string code)
        {
            var position = await UnitOfWork.Positions.GetByCodeAsync(code);
            return Mapper.Map<PositionDto>(position);
        }

        public async Task<PositionDto?> GetByNameAsync(string name)
        {
            var position = await UnitOfWork.Positions.GetByNameAsync(name);
            return Mapper.Map<PositionDto>(position);
        }

        public async Task<IEnumerable<PositionDto>> GetByDepartmentIdAsync(Guid departmentId)
        {
            var positions = await UnitOfWork.Positions.GetByDepartmentIdAsync(departmentId);
            return Mapper.Map<IEnumerable<PositionDto>>(positions);
        }

        public async Task<IEnumerable<PositionDto>> GetActivePositionsAsync()
        {
            var positions = await UnitOfWork.Positions.GetActivePositionsAsync();
            return Mapper.Map<IEnumerable<PositionDto>>(positions);
        }

        public async Task<PositionWithEmployeesDto?> GetWithEmployeeCountAsync(Guid id)
        {
            var position = await UnitOfWork.Positions.GetWithDepartmentAsync(id);
            if (position == null) return null;

            var dto = Mapper.Map<PositionWithEmployeesDto>(position);
            dto.EmployeeCount = await UnitOfWork.Positions.GetEmployeeCountAsync(id);
            return dto;
        }

        public async Task<PositionDto?> AssignDepartmentAsync(Guid positionId, Guid? departmentId)
        {
            var position = await UnitOfWork.Positions.GetByIdAsync(positionId);
            if (position == null)
            {
                throw new InvalidOperationException("Position Not Found");
            }

            if (departmentId.HasValue)
            {
                var department = await UnitOfWork.Departments.GetByIdAsync(departmentId.Value);
                if (department == null)
                {
                    throw new InvalidOperationException("Department not found.");
                }
            }

            position.DepartmentId = departmentId;
            await UnitOfWork.Positions.UpdateAsync(position);
            await UnitOfWork.SaveChangesAsync();

            return await GetByIdAsync(positionId);
        }

        protected override async Task ValidateCreateAsync(CreatePositionDto dto)
        {
            if (await UnitOfWork.Positions.PositionCodeExistsAsync(dto.Code))
            {
                throw new InvalidOperationException("Position code already exists.");
            }

            if (dto.DepartmentId.HasValue)
            {
                var department = await UnitOfWork.Departments.GetByIdAsync(dto.DepartmentId.Value);
                if (department == null)
                {
                    throw new InvalidOperationException("Department not found.");
                }
            }

            ValidateSalaryRange(dto.MinSalary, dto.MaxSalary);
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdatePositionDto dto, Position entity)
        {
            if (dto.Code != null && dto.Code != entity.Code)
            {
                if (await UnitOfWork.Positions.PositionCodeExistsAsync(dto.Code, id))
                {
                    throw new InvalidOperationException("Position code already exists.");
                }
            }

            if (dto.DepartmentId.HasValue)
            {
                var department = await UnitOfWork.Departments.GetByIdAsync(dto.DepartmentId.Value);
                if (department == null)
                {
                    throw new InvalidOperationException("Department not found.");
                }
            }

            var minSalary = dto.MinSalary ?? entity.MinSalary;
            var maxSalary = dto.MaxSalary ?? entity.MaxSalary;
            ValidateSalaryRange(minSalary, maxSalary);
        }

        protected override async Task ValidateDeleteAsync(Guid id, Position entity)
        {
            if (await UnitOfWork.Positions.HasEmployeesAsync(id))
            {
                throw new InvalidOperationException("Cannot delete position with employees. Please reassign employees first.");
            }
        }

        private static void ValidateSalaryRange(decimal? minSalary, decimal? maxSalary)
        {
            if (minSalary.HasValue && maxSalary.HasValue && minSalary > maxSalary)
            {
                throw new InvalidOperationException("Minimum salary cannot be greater than maximum salary.");
            }
        }
    }
}
