using AutoMapper;
using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Complaint;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Entities;

namespace cdr_group.Application.Services
{
    public class ComplaintService : BaseService<Complaint, ComplaintDto, CreateComplaintDto, UpdateComplaintDto>, IComplaintService
    {
        public ComplaintService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        protected override IRepository<Complaint> Repository => UnitOfWork.Complaints;

        public override async Task<PagedResult<ComplaintDto>> GetPagedAsync(PagedRequest request)
        {
            var (items, totalCount) = await UnitOfWork.Complaints.GetComplaintsPagedAsync(request);
            var dtos = Mapper.Map<List<ComplaintDto>>(items);
            return new PagedResult<ComplaintDto>(dtos, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
