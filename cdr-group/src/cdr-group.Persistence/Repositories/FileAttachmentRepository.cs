using Microsoft.EntityFrameworkCore;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Domain.Entities;
using cdr_group.Persistence.Data;

namespace cdr_group.Persistence.Repositories
{
    public class FileAttachmentRepository : Repository<FileAttachment>, IFileAttachmentRepository
    {
        public FileAttachmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FileAttachment>> GetByEntityIdAsync(Guid entityId)
        {
            return await _dbSet
                .Where(f => f.EntityId == entityId && !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<FileAttachment>> GetByEntityTypeAsync(string entityType)
        {
            return await _dbSet
                .Where(f => f.EntityType == entityType && !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<FileAttachment>> GetByEntityAsync(Guid entityId, string entityType)
        {
            return await _dbSet
                .Where(f => f.EntityId == entityId && f.EntityType == entityType && !f.IsDeleted)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }
    }
}
