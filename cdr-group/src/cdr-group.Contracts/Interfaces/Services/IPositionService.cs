using cdr_group.Contracts.DTOs.Position;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IPositionService : IBaseService<PositionDto, CreatePositionDto, UpdatePositionDto>
    {
        Task<PositionDto?> GetByCodeAsync(string code);
        Task<PositionDto?> GetByNameAsync(string name);
        Task<IEnumerable<PositionDto>> GetActivePositionsAsync();
        Task<PositionWithEmployeesDto?> GetWithEmployeeCountAsync(Guid id);
    }
}
