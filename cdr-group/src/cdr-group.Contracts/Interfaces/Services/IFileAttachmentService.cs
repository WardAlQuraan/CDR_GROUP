using cdr_group.Contracts.DTOs.FileAttachment;

namespace cdr_group.Contracts.Interfaces.Services
{
    public interface IFileAttachmentService : IBaseService<FileAttachmentDto, CreateFileAttachmentDto, UpdateFileAttachmentDto>
    {
        Task<IEnumerable<FileAttachmentDto>> GetByEntityIdAsync(Guid entityId);
        Task<IEnumerable<FileAttachmentDto>> GetByEntityTypeAsync(string entityType);
        Task<IEnumerable<FileAttachmentDto>> GetByEntityAsync(Guid entityId, string entityType);
        Task<IEnumerable<BulkFileOperationResultDto>> BulkOperationAsync(IEnumerable<BulkFileOperationItemDto> items);
    }
}
