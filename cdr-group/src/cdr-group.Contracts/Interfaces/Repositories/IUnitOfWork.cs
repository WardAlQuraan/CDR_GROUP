namespace cdr_group.Contracts.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IEmployeeRepository Employees { get; }
IPositionRepository Positions { get; }
        IFileAttachmentRepository FileAttachments { get; }
        IEventRepository Events { get; }
        ICompanyRepository Companies { get; }
        IContactUsRepository ContactUs { get; }
        ISalaryHistoryRepository SalaryHistories { get; }
        ICompanyContactRepository CompanyContacts { get; }
        ICompanyBackgroundRepository CompanyBackgrounds { get; }
        ICompanyFormRepository CompanyForms { get; }
        ICompanyPreferenceRepository CompanyPreferences { get; }
        ICompanyBranchRepository CompanyBranches { get; }
        ICompanyFinancialClausesRightsRepository CompanyFinancialClausesRights { get; }
        ICompanyClientReachRepository CompanyClientReaches { get; }
        ICompanyTitleDescriptionRepository CompanyTitleDescriptions { get; }
        IAuditLogRepository AuditLogs { get; }
        IReviewRepository Reviews { get; }
        IComplaintRepository Complaints { get; }
        ICountryRepository Countries { get; }
        ICityRepository Cities { get; }
        IPartnerRepository Partners { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
