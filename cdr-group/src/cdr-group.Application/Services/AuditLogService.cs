using AutoMapper;
using cdr_group.Contracts.DTOs.AuditLog;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;

namespace cdr_group.Application.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuditLogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<AuditLogDto>> GetPagedAsync(PagedRequest request, string? entityName = null, string? entityId = null)
        {
            var (items, totalCount) = await _unitOfWork.AuditLogs.GetPagedAsync(request, entityName, entityId);
            var dtos = _mapper.Map<List<AuditLogDto>>(items);
            return new PagedResult<AuditLogDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
