namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
        IPositionRepository Positions { get; }
        IFileAttachmentRepository FileAttachments { get; }
        IEventRepository Events { get; }
        ICompanyRepository Companies { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
