using cdr_group.Contracts.DTOs.Event;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IEventService : IBaseService<EventDto, CreateEventDto, UpdateEventDto>
    {
        Task<IEnumerable<EventDto>> GetByDepartmentIdAsync(Guid departmentId);
    }
}
