using cdr_group.Contracts.DTOs.Complaint;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IComplaintService : IBaseService<ComplaintDto, CreateComplaintDto, UpdateComplaintDto>
    {
    }
}
