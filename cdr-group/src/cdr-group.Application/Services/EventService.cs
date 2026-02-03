using AutoMapper;
using Microsoft.AspNetCore.Http;
using cdr_group.Application.Helpers;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Event;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class EventService : BaseService<Event, EventDto, CreateEventDto, UpdateEventDto>, IEventService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override IRepository<Event> Repository => UnitOfWork.Events;

        public override async Task<IEnumerable<EventDto>> GetAllAsync()
        {
            var (events, _) = await UnitOfWork.Events.GetEventsPagedAsync(new PagedRequest { PageSize = int.MaxValue });
            var eventDtos = Mapper.Map<List<EventDto>>(events);
            await PopulatePrimaryFileUrlsAsync(eventDtos);
            return eventDtos;
        }

        public override async Task<PagedResult<EventDto>> GetPagedAsync(PagedRequest request)
        {
            var (events, totalCount) = await UnitOfWork.Events.GetEventsPagedAsync(request);
            var eventDtos = Mapper.Map<List<EventDto>>(events);
            await PopulatePrimaryFileUrlsAsync(eventDtos);
            return new PagedResult<EventDto>(eventDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<EventDto?> GetByIdAsync(Guid id)
        {
            var eventEntity = await UnitOfWork.Events.GetWithDepartmentAsync(id);
            var dto = Mapper.Map<EventDto>(eventEntity);
            if (dto != null)
            {
                await PopulatePrimaryFileUrlAsync(dto);
            }
            return dto;
        }

        public async Task<IEnumerable<EventDto>> GetByDepartmentIdAsync(Guid departmentId)
        {
            var events = await UnitOfWork.Events.GetByDepartmentIdAsync(departmentId);
            var eventDtos = Mapper.Map<List<EventDto>>(events);
            await PopulatePrimaryFileUrlsAsync(eventDtos);
            return eventDtos;
        }

        protected override async Task ValidateCreateAsync(CreateEventDto dto)
        {
            if (dto.DepartmentId.HasValue)
            {
                var department = await UnitOfWork.Departments.GetByIdAsync(dto.DepartmentId.Value);
                if (department == null)
                {
                    throw new InvalidOperationException("Department not found.");
                }
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateEventDto dto, Event entity)
        {
            if (dto.DepartmentId.HasValue)
            {
                var department = await UnitOfWork.Departments.GetByIdAsync(dto.DepartmentId.Value);
                if (department == null)
                {
                    throw new InvalidOperationException("Department not found.");
                }
            }
        }

        private async Task PopulatePrimaryFileUrlAsync(EventDto dto)
        {
            var files = await UnitOfWork.FileAttachments.GetByEntityAsync(dto.Id, EntityTypes.Event);
            var file = files.FirstOrDefault(f => f.IsPrimary) ?? files.FirstOrDefault();
            dto.PrimaryFileUrl = UrlHelper.BuildFullUrl(file?.Path, _httpContextAccessor);
        }

        private async Task PopulatePrimaryFileUrlsAsync(List<EventDto> dtos)
        {
            foreach (var dto in dtos)
            {
                var files = await UnitOfWork.FileAttachments.GetByEntityAsync(dto.Id, EntityTypes.Event);
                var file = files.FirstOrDefault(f => f.IsPrimary) ?? files.FirstOrDefault();
                dto.PrimaryFileUrl = UrlHelper.BuildFullUrl(file?.Path, _httpContextAccessor);
            }
        }
    }
}
