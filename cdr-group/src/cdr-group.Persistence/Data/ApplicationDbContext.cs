using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Entities.Base;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Contracts.Interfaces.Services;
using PermissionConstants = cdr_group.Domain.Constants.Permissions;

namespace cdr_group.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentUserService? _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditFields();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            var currentTime = DateTime.UtcNow;
            var currentUser = _currentUserService?.Username;

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = currentTime;
                        entry.Entity.CreatedBy = currentUser;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = currentTime;
                        entry.Entity.UpdatedBy = currentUser;
                        break;
                }
            }
        }

        private static void ApplyGlobalFilters(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var filterExpression = Expression.Lambda(
                        Expression.Equal(property, Expression.Constant(false)),
                        parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filterExpression);
                }
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<FileAttachment> FileAttachments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ContactUs> ContactUsMessages { get; set; }
        public DbSet<SalaryHistory> SalaryHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply global query filter for soft delete on all BaseEntity-derived entities
            ApplyGlobalFilters(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            // Role configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Permission configuration
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Module).HasMaxLength(100);
            });

            // UserRole configuration (many-to-many)
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // RolePermission configuration (many-to-many)
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.RoleId, e.PermissionId }).IsUnique();

                entity.HasOne(e => e.Role)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(e => e.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // RefreshToken configuration
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Token).IsUnique();
                entity.Property(e => e.Token).IsRequired().HasMaxLength(500);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.RefreshTokens)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Employee configuration
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.EmployeeCode).IsUnique();
                entity.Property(e => e.EmployeeCode).IsRequired().HasMaxLength(50);
                entity.Property(e => e.FirstNameEn).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastNameEn).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstNameAr).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastNameAr).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Salary).HasPrecision(18, 2);

                // Department relationship (required)
                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employees)
                    .HasForeignKey(e => e.DepartmentId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                // Position relationship
                entity.HasOne(e => e.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(e => e.PositionId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Self-referencing relationship for manager
                entity.HasOne(e => e.Manager)
                    .WithMany(e => e.Subordinates)
                    .HasForeignKey(e => e.ManagerId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Optional relationship to User
                entity.HasOne(e => e.User)
                    .WithOne(u => u.Employee)
                    .HasForeignKey<Employee>(e => e.UserId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Company configuration
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Code).IsUnique();
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
                entity.Property(e => e.NameAr).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DescriptionEn).HasMaxLength(500);
                entity.Property(e => e.DescriptionAr).HasMaxLength(500);
            });

            // Department configuration
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.Code, e.CompanyId }).IsUnique();
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
                entity.Property(e => e.NameAr).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DescriptionEn).HasMaxLength(500);
                entity.Property(e => e.DescriptionAr).HasMaxLength(500);

                // Company relationship (required)
                entity.HasOne(e => e.Company)
                    .WithMany(c => c.Departments)
                    .HasForeignKey(e => e.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                // Self-referencing relationship for parent department
                entity.HasOne(e => e.ParentDepartment)
                    .WithMany(e => e.SubDepartments)
                    .HasForeignKey(e => e.ParentDepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Manager relationship
                entity.HasOne(e => e.Manager)
                    .WithMany()
                    .HasForeignKey(e => e.ManagerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Position configuration
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Code).IsUnique();
                entity.Property(e => e.Code).IsRequired().HasMaxLength(50);
                entity.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
                entity.Property(e => e.NameAr).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DescriptionEn).HasMaxLength(500);
                entity.Property(e => e.DescriptionAr).HasMaxLength(500);
                entity.Property(e => e.MinSalary).HasPrecision(18, 2);
                entity.Property(e => e.MaxSalary).HasPrecision(18, 2);

                // Department relationship (optional)
                entity.HasOne(e => e.Department)
                    .WithMany()
                    .HasForeignKey(e => e.DepartmentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // FileAttachment configuration
            modelBuilder.Entity<FileAttachment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.EntityId);
                entity.HasIndex(e => e.EntityType);
                entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.StoredFileName).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Path).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ContentType).IsRequired().HasMaxLength(100);
                entity.Property(e => e.EntityType).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Event configuration
            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TitleEn).IsRequired().HasMaxLength(200);
                entity.Property(e => e.TitleAr).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DescriptionEn).HasMaxLength(2000);
                entity.Property(e => e.DescriptionAr).HasMaxLength(2000);
                entity.Property(e => e.EventUrl).HasMaxLength(500);

                // Company relationship (required)
                entity.HasOne(e => e.Company)
                    .WithMany(c => c.Events)
                    .HasForeignKey(e => e.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ContactUs configuration
            modelBuilder.Entity<ContactUs>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(2000);
            });

            // SalaryHistory configuration
            modelBuilder.Entity<SalaryHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.EmployeeId);
                entity.Property(e => e.OldSalary).HasPrecision(18, 2);
                entity.Property(e => e.NewSalary).HasPrecision(18, 2);
                entity.Property(e => e.Reason).HasMaxLength(500);

                entity.HasOne(e => e.Employee)
                    .WithMany(e => e.SalaryHistories)
                    .HasForeignKey(e => e.EmployeeId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed default data
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            var superAdminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var adminRoleId = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var userRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var adminUserId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            // Permission IDs
            var usersReadId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var usersCreateId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var usersUpdateId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var usersDeleteId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
            var usersActivateId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var rolesReadId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
            var rolesManageId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");

            // Employee Permission IDs
            var employeesReadId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var employeesCreateId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var employeesUpdateId = Guid.Parse("22222222-cccc-cccc-cccc-cccccccccccc");
            var employeesDeleteId = Guid.Parse("22222222-dddd-dddd-dddd-dddddddddddd");
            var employeesAssignManagerId = Guid.Parse("22222222-eeee-eeee-eeee-eeeeeeeeeeee");
            var employeesLinkToUserId = Guid.Parse("22222222-ffff-ffff-ffff-ffffffffffff");

            // Department Permission IDs
            var departmentsReadId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var departmentsCreateId = Guid.Parse("33333333-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var departmentsUpdateId = Guid.Parse("33333333-cccc-cccc-cccc-cccccccccccc");
            var departmentsDeleteId = Guid.Parse("33333333-dddd-dddd-dddd-dddddddddddd");
            var departmentsAssignManagerId = Guid.Parse("33333333-eeee-eeee-eeee-eeeeeeeeeeee");

            // Position Permission IDs
            var positionsReadId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var positionsCreateId = Guid.Parse("55555555-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var positionsUpdateId = Guid.Parse("55555555-cccc-cccc-cccc-cccccccccccc");
            var positionsDeleteId = Guid.Parse("55555555-dddd-dddd-dddd-dddddddddddd");

            // File Permission IDs
            var filesReadId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var filesUploadId = Guid.Parse("77777777-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var filesUpdateId = Guid.Parse("77777777-cccc-cccc-cccc-cccccccccccc");
            var filesDeleteId = Guid.Parse("77777777-dddd-dddd-dddd-dddddddddddd");

            // Company Permission IDs
            var companiesReadId = Guid.Parse("99999999-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var companiesCreateId = Guid.Parse("99999999-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var companiesUpdateId = Guid.Parse("99999999-cccc-cccc-cccc-cccccccccccc");
            var companiesDeleteId = Guid.Parse("99999999-dddd-dddd-dddd-dddddddddddd");

            // Event Permission IDs
            var eventsReadId = Guid.Parse("88888888-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var eventsCreateId = Guid.Parse("88888888-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var eventsUpdateId = Guid.Parse("88888888-cccc-cccc-cccc-cccccccccccc");
            var eventsDeleteId = Guid.Parse("88888888-dddd-dddd-dddd-dddddddddddd");

            // ContactUs Permission IDs
            var contactusReadId = Guid.Parse("aabbccdd-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var contactusUpdateId = Guid.Parse("aabbccdd-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var contactusDeleteId = Guid.Parse("aabbccdd-cccc-cccc-cccc-cccccccccccc");

            // SalaryHistory Permission IDs
            var salaryHistoriesReadId = Guid.Parse("aabbccdd-1111-1111-1111-aaaaaaaaaaaa");
            var salaryHistoriesCreateId = Guid.Parse("aabbccdd-1111-1111-1111-bbbbbbbbbbbb");
            var salaryHistoriesUpdateId = Guid.Parse("aabbccdd-1111-1111-1111-cccccccccccc");
            var salaryHistoriesDeleteId = Guid.Parse("aabbccdd-1111-1111-1111-dddddddddddd");

            // Department IDs
            var itDepartmentId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var hrDepartmentId = Guid.Parse("44444444-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var financeDepartmentId = Guid.Parse("44444444-cccc-cccc-cccc-cccccccccccc");
            var operationsDepartmentId = Guid.Parse("44444444-dddd-dddd-dddd-dddddddddddd");
            var marketingDepartmentId = Guid.Parse("44444444-eeee-eeee-eeee-eeeeeeeeeeee");

            // Company IDs
            var cdrGroupCompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff");

            // Position IDs
            var seniorDeveloperId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var juniorDeveloperId = Guid.Parse("66666666-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var hrManagerId = Guid.Parse("66666666-cccc-cccc-cccc-cccccccccccc");
            var hrSpecialistId = Guid.Parse("66666666-dddd-dddd-dddd-dddddddddddd");
            var accountantId = Guid.Parse("66666666-eeee-eeee-eeee-eeeeeeeeeeee");
            var projectManagerId = Guid.Parse("66666666-ffff-ffff-ffff-ffffffffffff");

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = superAdminRoleId,
                    Name = "Super Admin",
                    Description = "Super Administrator role with full access including user deletion",
                    IsSystemRole = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Role
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    Description = "Administrator role with limited user management",
                    IsSystemRole = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Role
                {
                    Id = userRoleId,
                    Name = "User",
                    Description = "Standard user role",
                    IsSystemRole = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
            );

            // Seed default permissions
            var permissions = new[]
            {
                new Permission { Id = usersReadId, Name = PermissionConstants.Users.Read, Description = "View users", Module = "Users", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = usersCreateId, Name = PermissionConstants.Users.Create, Description = "Create users", Module = "Users", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = usersUpdateId, Name = PermissionConstants.Users.Update, Description = "Update users", Module = "Users", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = usersDeleteId, Name = PermissionConstants.Users.Delete, Description = "Delete users", Module = "Users", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = usersActivateId, Name = PermissionConstants.Users.Activate, Description = "Activate/Deactivate users", Module = "Users", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = rolesReadId, Name = PermissionConstants.Roles.Read, Description = "View roles", Module = "Roles", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = rolesManageId, Name = PermissionConstants.Roles.Manage, Description = "Manage roles", Module = "Roles", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = employeesReadId, Name = PermissionConstants.Employees.Read, Description = "View employees", Module = "Employees", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = employeesCreateId, Name = PermissionConstants.Employees.Create, Description = "Create employees", Module = "Employees", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = employeesUpdateId, Name = PermissionConstants.Employees.Update, Description = "Update employees", Module = "Employees", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = employeesDeleteId, Name = PermissionConstants.Employees.Delete, Description = "Delete employees", Module = "Employees", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = employeesAssignManagerId, Name = PermissionConstants.Employees.AssignManager, Description = "Assign manager to employees", Module = "Employees", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = employeesLinkToUserId, Name = PermissionConstants.Employees.LinkToUser, Description = "Link employees to user accounts", Module = "Employees", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // Department permissions
                new Permission { Id = departmentsReadId, Name = PermissionConstants.Departments.Read, Description = "View departments", Module = "Departments", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = departmentsCreateId, Name = PermissionConstants.Departments.Create, Description = "Create departments", Module = "Departments", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = departmentsUpdateId, Name = PermissionConstants.Departments.Update, Description = "Update departments", Module = "Departments", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = departmentsDeleteId, Name = PermissionConstants.Departments.Delete, Description = "Delete departments", Module = "Departments", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = departmentsAssignManagerId, Name = PermissionConstants.Departments.AssignManager, Description = "Assign manager to departments", Module = "Departments", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // Position permissions
                new Permission { Id = positionsReadId, Name = PermissionConstants.Positions.Read, Description = "View positions", Module = "Positions", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = positionsCreateId, Name = PermissionConstants.Positions.Create, Description = "Create positions", Module = "Positions", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = positionsUpdateId, Name = PermissionConstants.Positions.Update, Description = "Update positions", Module = "Positions", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = positionsDeleteId, Name = PermissionConstants.Positions.Delete, Description = "Delete positions", Module = "Positions", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // File permissions
                new Permission { Id = filesReadId, Name = PermissionConstants.Files.Read, Description = "View files", Module = "Files", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = filesUploadId, Name = PermissionConstants.Files.Upload, Description = "Upload files", Module = "Files", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = filesUpdateId, Name = PermissionConstants.Files.Update, Description = "Update files", Module = "Files", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = filesDeleteId, Name = PermissionConstants.Files.Delete, Description = "Delete files", Module = "Files", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // Company permissions
                new Permission { Id = companiesReadId, Name = PermissionConstants.Companies.Read, Description = "View companies", Module = "Companies", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companiesCreateId, Name = PermissionConstants.Companies.Create, Description = "Create companies", Module = "Companies", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companiesUpdateId, Name = PermissionConstants.Companies.Update, Description = "Update companies", Module = "Companies", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companiesDeleteId, Name = PermissionConstants.Companies.Delete, Description = "Delete companies", Module = "Companies", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // Event permissions
                new Permission { Id = eventsReadId, Name = PermissionConstants.Events.Read, Description = "View events", Module = "Events", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = eventsCreateId, Name = PermissionConstants.Events.Create, Description = "Create events", Module = "Events", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = eventsUpdateId, Name = PermissionConstants.Events.Update, Description = "Update events", Module = "Events", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = eventsDeleteId, Name = PermissionConstants.Events.Delete, Description = "Delete events", Module = "Events", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // ContactUs permissions
                new Permission { Id = contactusReadId, Name = PermissionConstants.ContactUs.Read, Description = "View contact us messages", Module = "ContactUs", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = contactusUpdateId, Name = PermissionConstants.ContactUs.Update, Description = "Update contact us messages", Module = "ContactUs", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = contactusDeleteId, Name = PermissionConstants.ContactUs.Delete, Description = "Delete contact us messages", Module = "ContactUs", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // SalaryHistory permissions
                new Permission { Id = salaryHistoriesReadId, Name = PermissionConstants.SalaryHistories.Read, Description = "View salary histories", Module = "SalaryHistories", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = salaryHistoriesCreateId, Name = PermissionConstants.SalaryHistories.Create, Description = "Create salary histories", Module = "SalaryHistories", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = salaryHistoriesUpdateId, Name = PermissionConstants.SalaryHistories.Update, Description = "Update salary histories", Module = "SalaryHistories", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = salaryHistoriesDeleteId, Name = PermissionConstants.SalaryHistories.Delete, Description = "Delete salary histories", Module = "SalaryHistories", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            };

            modelBuilder.Entity<Permission>().HasData(permissions);

            // Assign all permissions to Super Admin role
            var superAdminRolePermissions = permissions.Select((p, index) => new RolePermission
            {
                Id = Guid.Parse($"00000000-0000-0000-0000-{(index + 1):D12}"),
                RoleId = superAdminRoleId,
                PermissionId = p.Id,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            }).ToArray();

            modelBuilder.Entity<RolePermission>().HasData(superAdminRolePermissions);

            // Assign permissions to Admin role (exclude users.delete, users.activate, and users.update)
            var adminPermissions = permissions.Where(p => p.Id != usersDeleteId && p.Id != usersActivateId && p.Id != usersUpdateId).ToArray();
            var adminRolePermissions = adminPermissions.Select((p, index) => new RolePermission
            {
                Id = Guid.Parse($"00000000-0000-0000-1111-{(index + 1):D12}"),
                RoleId = adminRoleId,
                PermissionId = p.Id,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            }).ToArray();

            modelBuilder.Entity<RolePermission>().HasData(adminRolePermissions);

            // Seed admin user (username: admin, password: cdrGroup123!)
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminUserId,
                Username = "admin",
                Email = "admin@cdrgroup.com",
                PasswordHash = "$2a$11$oTDZps5ZuNoGttww.CywKero5c9rhBPEC2LRqYCc0ueL8VDIZEL/.",
                FirstName = "System",
                LastName = "Administrator",
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            });

            // Assign Super Admin role to admin user
            modelBuilder.Entity<UserRole>().HasData(new UserRole
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                UserId = adminUserId,
                RoleId = superAdminRoleId,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            });

            // Seed default departments
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = cdrGroupCompanyId,
                    Code = "CDR",
                    NameEn = "CDR Group",
                    NameAr = "مجموعة سي دي آر",
                    DescriptionEn = "CDR Group Company",
                    DescriptionAr = "شركة مجموعة سي دي آر",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = itDepartmentId,
                    Code = "IT",
                    NameEn = "Information Technology",
                    NameAr = "تكنولوجيا المعلومات",
                    DescriptionEn = "IT and Software Development department",
                    DescriptionAr = "قسم تكنولوجيا المعلومات وتطوير البرمجيات",
                    IsActive = true,
                    CompanyId = cdrGroupCompanyId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Department
                {
                    Id = hrDepartmentId,
                    Code = "HR",
                    NameEn = "Human Resources",
                    NameAr = "الموارد البشرية",
                    DescriptionEn = "Human Resources and Personnel department",
                    DescriptionAr = "قسم الموارد البشرية وشؤون الموظفين",
                    IsActive = true,
                    CompanyId = cdrGroupCompanyId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Department
                {
                    Id = financeDepartmentId,
                    Code = "FIN",
                    NameEn = "Finance",
                    NameAr = "المالية",
                    DescriptionEn = "Finance and Accounting department",
                    DescriptionAr = "قسم المالية والمحاسبة",
                    IsActive = true,
                    CompanyId = cdrGroupCompanyId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Department
                {
                    Id = operationsDepartmentId,
                    Code = "OPS",
                    NameEn = "Operations",
                    NameAr = "العمليات",
                    DescriptionEn = "Operations and Logistics department",
                    DescriptionAr = "قسم العمليات والخدمات اللوجستية",
                    IsActive = true,
                    CompanyId = cdrGroupCompanyId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Department
                {
                    Id = marketingDepartmentId,
                    Code = "MKT",
                    NameEn = "Marketing",
                    NameAr = "التسويق",
                    DescriptionEn = "Marketing and Sales department",
                    DescriptionAr = "قسم التسويق والمبيعات",
                    IsActive = true,
                    CompanyId = cdrGroupCompanyId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
            );

            // Seed default positions
            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    Id = seniorDeveloperId,
                    Code = "SR-DEV",
                    NameEn = "Senior Developer",
                    NameAr = "مطور أول",
                    DescriptionEn = "Senior software developer responsible for complex technical tasks",
                    DescriptionAr = "مطور برمجيات أول مسؤول عن المهام التقنية المعقدة",
                    MinSalary = 8000,
                    MaxSalary = 15000,
                    IsActive = true,
                    DepartmentId = itDepartmentId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Position
                {
                    Id = juniorDeveloperId,
                    Code = "JR-DEV",
                    NameEn = "Junior Developer",
                    NameAr = "مطور مبتدئ",
                    DescriptionEn = "Junior software developer learning and growing skills",
                    DescriptionAr = "مطور برمجيات مبتدئ يتعلم ويطور مهاراته",
                    MinSalary = 3000,
                    MaxSalary = 6000,
                    IsActive = true,
                    DepartmentId = itDepartmentId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Position
                {
                    Id = hrManagerId,
                    Code = "HR-MGR",
                    NameEn = "HR Manager",
                    NameAr = "مدير الموارد البشرية",
                    DescriptionEn = "Human resources department manager",
                    DescriptionAr = "مدير قسم الموارد البشرية",
                    MinSalary = 10000,
                    MaxSalary = 18000,
                    IsActive = true,
                    DepartmentId = hrDepartmentId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Position
                {
                    Id = hrSpecialistId,
                    Code = "HR-SPEC",
                    NameEn = "HR Specialist",
                    NameAr = "أخصائي موارد بشرية",
                    DescriptionEn = "Human resources specialist handling recruitment and employee relations",
                    DescriptionAr = "أخصائي موارد بشرية يتعامل مع التوظيف وعلاقات الموظفين",
                    MinSalary = 4000,
                    MaxSalary = 8000,
                    IsActive = true,
                    DepartmentId = hrDepartmentId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Position
                {
                    Id = accountantId,
                    Code = "ACCT",
                    NameEn = "Accountant",
                    NameAr = "محاسب",
                    DescriptionEn = "Financial accountant responsible for financial records",
                    DescriptionAr = "محاسب مالي مسؤول عن السجلات المالية",
                    MinSalary = 5000,
                    MaxSalary = 10000,
                    IsActive = true,
                    DepartmentId = financeDepartmentId,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                new Position
                {
                    Id = projectManagerId,
                    Code = "PM",
                    NameEn = "Project Manager",
                    NameAr = "مدير مشروع",
                    DescriptionEn = "Project manager responsible for project planning and execution",
                    DescriptionAr = "مدير مشروع مسؤول عن تخطيط وتنفيذ المشاريع",
                    MinSalary = 9000,
                    MaxSalary = 16000,
                    IsActive = true,
                    DepartmentId = null,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
            );
        }
    }
}
