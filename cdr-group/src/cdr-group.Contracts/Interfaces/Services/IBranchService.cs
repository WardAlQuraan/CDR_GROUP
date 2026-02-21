using cdr_group.Contracts.DTOs.Branch;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IBranchService : IBaseService<BranchDto, CreateBranchDto, UpdateBranchDto>
    {
        Task<BranchDto?> GetByCodeAsync(string code);
        Task<IEnumerable<BranchDto>> GetActiveAsync();
        Task<IEnumerable<BranchDto>> GetByCompanyIdAsync(Guid companyId);
    }
}
