using cdr_group.Domain.Entities;

namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IFileAttachmentRepository : IRepository<FileAttachment>
    {
        Task<IEnumerable<FileAttachment>> GetByEntityIdAsync(Guid entityId);
        Task<IEnumerable<FileAttachment>> GetByEntityTypeAsync(string entityType);
        Task<IEnumerable<FileAttachment>> GetByEntityAsync(Guid entityId, string entityType);
    }
}
