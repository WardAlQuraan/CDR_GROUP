using cdr_group.Contracts.DTOs.Common;
using cdr_group.Contracts.DTOs.Complaint;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IComplaintService : IBaseService<ComplaintDto, CreateComplaintDto, UpdateComplaintDto>
    {
        Task<PagedResult<ComplaintDto>> GetComplaintsPagedAsync(ComplaintPagedRequest request);
    }
}
