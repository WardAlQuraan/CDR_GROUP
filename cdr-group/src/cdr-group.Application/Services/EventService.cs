using AutoMapper;
using Microsoft.AspNetCore.Http;
using cdr_group.Application.Helpers;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Event;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Localization;

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
            var (events, _) = await UnitOfWork.Events.GetEventsPagedAsync(new EventPagedRequest { PageSize = int.MaxValue });
            var eventDtos = Mapper.Map<List<EventDto>>(events);
            await PopulatePrimaryFileUrlsAsync(eventDtos);
            return eventDtos;
        }

        public override async Task<PagedResult<EventDto>> GetPagedAsync(PagedRequest request)
        {
            var eventRequest = request as EventPagedRequest ?? new EventPagedRequest
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SearchTerm = request.SearchTerm,
                SortBy = request.SortBy,
                SortDescending = request.SortDescending,
                SearchProperties = request.SearchProperties
            };

            if (!eventRequest.CompanyId.HasValue && !string.IsNullOrWhiteSpace(eventRequest.CompanyCode))
            {
                var company = await UnitOfWork.Companies.GetByCodeAsync(eventRequest.CompanyCode);
                eventRequest.CompanyId = company?.Id;
            }

            var (events, totalCount) = await UnitOfWork.Events.GetEventsPagedAsync(eventRequest);
            var eventDtos = Mapper.Map<List<EventDto>>(events);
            await PopulatePrimaryFileUrlsAsync(eventDtos);
            return new PagedResult<EventDto>(eventDtos, totalCount, request.PageNumber, request.PageSize);
        }

        public override async Task<EventDto?> GetByIdAsync(Guid id)
        {
            var eventEntity = await UnitOfWork.Events.GetWithCompanyAsync(id);
            var dto = Mapper.Map<EventDto>(eventEntity);
            if (dto != null)
            {
                await PopulatePrimaryFileUrlAsync(dto);
            }
            return dto;
        }

        public async Task<IEnumerable<EventDto>> GetByCompanyIdAsync(Guid companyId)
        {
            var events = await UnitOfWork.Events.GetByCompanyIdAsync(companyId);
            var eventDtos = Mapper.Map<List<EventDto>>(events);
            await PopulatePrimaryFileUrlsAsync(eventDtos);
            return eventDtos;
        }

        protected override async Task ValidateCreateAsync(CreateEventDto dto)
        {
            if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId))
            {
                throw new InvalidOperationException(Messages.CompanyNotFound);
            }
        }

        protected override async Task ValidateUpdateAsync(Guid id, UpdateEventDto dto, Event entity)
        {
            if (dto.CompanyId.HasValue)
            {
                if (!await UnitOfWork.Companies.ExistsAsync(dto.CompanyId.Value))
                {
                    throw new InvalidOperationException(Messages.CompanyNotFound);
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
