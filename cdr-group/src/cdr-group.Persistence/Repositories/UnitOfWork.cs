using Microsoft.EntityFrameworkCore.Storage;
using cdr_group.Contracts.Interfaces.Repositories;
using cdr_group.Persistence.Data;

namespace cdr_group.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        private IUserRepository? _users;
        private IRoleRepository? _roles;
        private IPermissionRepository? _permissions;
        private IEmployeeRepository? _employees;
        private IPositionRepository? _positions;
        private IFileAttachmentRepository? _fileAttachments;
        private IEventRepository? _events;
        private ICompanyRepository? _companies;
        private IContactUsRepository? _contactUs;
        private ISalaryHistoryRepository? _salaryHistories;
        private ICompanyContactRepository? _companyContacts;
        private ICompanyBackgroundRepository? _companyBackgrounds;
        private IAuditLogRepository? _auditLogs;
        private IReviewRepository? _reviews;
        private IComplaintRepository? _complaints;
        private ICountryRepository? _countries;
        private ICityRepository? _cities;
        private IPartnerRepository? _partners;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _users ??= new UserRepository(_context);
        public IRoleRepository Roles => _roles ??= new RoleRepository(_context);
        public IPermissionRepository Permissions => _permissions ??= new PermissionRepository(_context);
        public IEmployeeRepository Employees => _employees ??= new EmployeeRepository(_context);
        public IPositionRepository Positions => _positions ??= new PositionRepository(_context);
        public IFileAttachmentRepository FileAttachments => _fileAttachments ??= new FileAttachmentRepository(_context);
        public IEventRepository Events => _events ??= new EventRepository(_context);
        public ICompanyRepository Companies => _companies ??= new CompanyRepository(_context);
        public IContactUsRepository ContactUs => _contactUs ??= new ContactUsRepository(_context);
        public ISalaryHistoryRepository SalaryHistories => _salaryHistories ??= new SalaryHistoryRepository(_context);
        public ICompanyContactRepository CompanyContacts => _companyContacts ??= new CompanyContactRepository(_context);
        public ICompanyBackgroundRepository CompanyBackgrounds => _companyBackgrounds ??= new CompanyBackgroundRepository(_context);
        public IAuditLogRepository AuditLogs => _auditLogs ??= new AuditLogRepository(_context);
        public IReviewRepository Reviews => _reviews ??= new ReviewRepository(_context);
        public IComplaintRepository Complaints => _complaints ??= new ComplaintRepository(_context);
        public ICountryRepository Countries => _countries ??= new CountryRepository(_context);
        public ICityRepository Cities => _cities ??= new CityRepository(_context);
        public IPartnerRepository Partners => _partners ??= new PartnerRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
