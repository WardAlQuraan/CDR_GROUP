using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Entities.Base;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;
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

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetAuditFields();
            var auditEntries = OnBeforeSaveChanges();
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            OnAfterSaveChanges(auditEntries, default).GetAwaiter().GetResult();
            return result;
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            await OnAfterSaveChanges(auditEntries, cancellationToken);
            return result;
        }

        private void SetAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            var currentTime = DateTime.Now;
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

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            var currentTime = DateTime.UtcNow;
            var currentUser = _currentUserService?.Username;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry)
                {
                    EntityName = entry.Entity.GetType().Name,
                    PerformedBy = currentUser,
                    Timestamp = currentTime
                };

                var baseEntityProperties = typeof(BaseEntity).GetProperties()
                    .Select(p => p.Name)
                    .ToHashSet();

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.ActionType = AuditActionTypes.Insert;
                        foreach (var property in entry.Properties)
                        {
                            if (baseEntityProperties.Contains(property.Metadata.Name))
                                continue;
                            if (property.IsTemporary)
                            {
                                auditEntry.TemporaryProperties.Add(property);
                                continue;
                            }
                            auditEntry.NewValues[property.Metadata.Name] = FormatAuditValue(property.CurrentValue);
                        }
                        break;

                    case EntityState.Modified:
                        var isDeletedProp = entry.Property(nameof(BaseEntity.IsDeleted));
                        bool isSoftDelete = isDeletedProp.IsModified
                            && (bool)(isDeletedProp.OriginalValue ?? false) == false
                            && (bool)(isDeletedProp.CurrentValue ?? false) == true;

                        auditEntry.ActionType = isSoftDelete
                            ? AuditActionTypes.Delete
                            : AuditActionTypes.Update;

                        foreach (var property in entry.Properties)
                        {
                            if (baseEntityProperties.Contains(property.Metadata.Name))
                                continue;
                            if (!property.IsModified)
                                continue;

                            var oldValue = property.OriginalValue;
                            var newValue = property.CurrentValue;
                            if (!Equals(oldValue, newValue))
                            {
                                auditEntry.AffectedColumns.Add(property.Metadata.Name);
                                auditEntry.OldValues[property.Metadata.Name] = FormatAuditValue(oldValue);
                                auditEntry.NewValues[property.Metadata.Name] = FormatAuditValue(newValue);
                            }
                        }
                        break;

                    case EntityState.Deleted:
                        auditEntry.ActionType = AuditActionTypes.Delete;
                        foreach (var property in entry.Properties)
                        {
                            if (baseEntityProperties.Contains(property.Metadata.Name))
                                continue;
                            auditEntry.OldValues[property.Metadata.Name] = FormatAuditValue(property.OriginalValue);
                        }
                        break;
                }

                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }

        private async Task OnAfterSaveChanges(List<AuditEntry> auditEntries, CancellationToken cancellationToken)
        {
            if (auditEntries.Count == 0)
                return;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var temporaryProperty in auditEntry.TemporaryProperties)
                {
                    auditEntry.NewValues[temporaryProperty.Metadata.Name] = temporaryProperty.CurrentValue;
                }
            }

            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAuditLog());
            }

            await base.SaveChangesAsync(true, cancellationToken);
        }

        private static object? FormatAuditValue(object? value)
        {
            return value switch
            {
                DateTime dt => dt.ToString("yyyy/MM/dd hh:mm:ss"),
                DateTimeOffset dto => dto.ToString("yyyy/MM/dd hh:mm:ss"),
                _ => value
            };
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
        public DbSet<Position> Positions { get; set; }
        public DbSet<FileAttachment> FileAttachments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ContactUs> ContactUsMessages { get; set; }
        public DbSet<SalaryHistory> SalaryHistories { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Complaint> Complaints { get; set; }

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

                // Company relationship (optional)
                entity.HasOne(e => e.Company)
                    .WithMany(c => c.Employees)
                    .HasForeignKey(e => e.CompanyId)
                    .OnDelete(DeleteBehavior.SetNull);

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
                entity.Property(e => e.StoryEn).HasMaxLength(2000);
                entity.Property(e => e.StoryAr).HasMaxLength(2000);
                entity.Property(e => e.MissionEn).HasMaxLength(1000);
                entity.Property(e => e.MissionAr).HasMaxLength(1000);
                entity.Property(e => e.TitleEn).HasMaxLength(500);
                entity.Property(e => e.TitleAr).HasMaxLength(500);
                entity.Property(e => e.VisionEn).HasMaxLength(1000);
                entity.Property(e => e.VisionAr).HasMaxLength(1000);
                entity.Property(e => e.PrimaryColor).HasMaxLength(20);
                entity.Property(e => e.SecondaryColor).HasMaxLength(20);

                entity.HasOne(e => e.Parent)
                    .WithMany(e => e.Children)
                    .HasForeignKey(e => e.ParentId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // CompanyContact configuration
            modelBuilder.Entity<CompanyContact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Icon).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Value).IsRequired().HasMaxLength(500);

                entity.HasOne(e => e.Company)
                    .WithMany(e => e.CompanyContacts)
                    .HasForeignKey(e => e.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
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

                entity.HasOne(e => e.Company)
                    .WithMany()
                    .HasForeignKey(e => e.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Review configuration
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Comment).IsRequired().HasMaxLength(2000);

                entity.HasOne(e => e.Company)
                    .WithMany()
                    .HasForeignKey(e => e.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Complaint configuration
            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Subject).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(2000);

                entity.HasOne(e => e.Company)
                    .WithMany()
                    .HasForeignKey(e => e.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
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

            // AuditLog configuration
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EntityName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.EntityId).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ActionType).IsRequired().HasMaxLength(20);
                entity.Property(e => e.OldValues).HasColumnType("longtext");
                entity.Property(e => e.NewValues).HasColumnType("longtext");
                entity.Property(e => e.AffectedColumns).HasColumnType("longtext");
                entity.Property(e => e.PerformedBy).HasMaxLength(200);
                entity.Property(e => e.Timestamp).IsRequired();

                entity.HasIndex(e => new { e.EntityName, e.EntityId });
                entity.HasIndex(e => e.Timestamp);
                entity.HasIndex(e => e.PerformedBy);
                entity.HasIndex(e => e.ActionType);
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

            // CompanyContact Permission IDs
            var companyContactsReadId = Guid.Parse("aabbccdd-3333-3333-3333-aaaaaaaaaaaa");
            var companyContactsCreateId = Guid.Parse("aabbccdd-3333-3333-3333-bbbbbbbbbbbb");
            var companyContactsUpdateId = Guid.Parse("aabbccdd-3333-3333-3333-cccccccccccc");
            var companyContactsDeleteId = Guid.Parse("aabbccdd-3333-3333-3333-dddddddddddd");

            // AuditLog Permission IDs
            var auditLogsReadId = Guid.Parse("aabbccdd-2222-2222-2222-aaaaaaaaaaaa");

            // Review Permission IDs
            var reviewsReadId = Guid.Parse("aabbccdd-4444-4444-4444-aaaaaaaaaaaa");
            var reviewsCreateId = Guid.Parse("aabbccdd-4444-4444-4444-bbbbbbbbbbbb");
            var reviewsUpdateId = Guid.Parse("aabbccdd-4444-4444-4444-cccccccccccc");
            var reviewsDeleteId = Guid.Parse("aabbccdd-4444-4444-4444-dddddddddddd");

            // Complaint Permission IDs
            var complaintsReadId = Guid.Parse("aabbccdd-5555-5555-5555-aaaaaaaaaaaa");
            var complaintsCreateId = Guid.Parse("aabbccdd-5555-5555-5555-bbbbbbbbbbbb");
            var complaintsUpdateId = Guid.Parse("aabbccdd-5555-5555-5555-cccccccccccc");
            var complaintsDeleteId = Guid.Parse("aabbccdd-5555-5555-5555-dddddddddddd");

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
                // CompanyContact permissions
                new Permission { Id = companyContactsReadId, Name = PermissionConstants.CompanyContacts.Read, Description = "View company contacts", Module = "CompanyContacts", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyContactsCreateId, Name = PermissionConstants.CompanyContacts.Create, Description = "Create company contacts", Module = "CompanyContacts", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyContactsUpdateId, Name = PermissionConstants.CompanyContacts.Update, Description = "Update company contacts", Module = "CompanyContacts", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyContactsDeleteId, Name = PermissionConstants.CompanyContacts.Delete, Description = "Delete company contacts", Module = "CompanyContacts", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // AuditLog permissions
                new Permission { Id = auditLogsReadId, Name = PermissionConstants.AuditLogs.Read, Description = "View audit logs", Module = "AuditLogs", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // Review permissions
                new Permission { Id = reviewsReadId, Name = PermissionConstants.Reviews.Read, Description = "View reviews", Module = "Reviews", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = reviewsCreateId, Name = PermissionConstants.Reviews.Create, Description = "Create reviews", Module = "Reviews", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = reviewsUpdateId, Name = PermissionConstants.Reviews.Update, Description = "Update reviews", Module = "Reviews", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = reviewsDeleteId, Name = PermissionConstants.Reviews.Delete, Description = "Delete reviews", Module = "Reviews", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // Complaint permissions
                new Permission { Id = complaintsReadId, Name = PermissionConstants.Complaints.Read, Description = "View complaints", Module = "Complaints", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = complaintsCreateId, Name = PermissionConstants.Complaints.Create, Description = "Create complaints", Module = "Complaints", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = complaintsUpdateId, Name = PermissionConstants.Complaints.Update, Description = "Update complaints", Module = "Complaints", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = complaintsDeleteId, Name = PermissionConstants.Complaints.Delete, Description = "Delete complaints", Module = "Complaints", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
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

            // Seed default company
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = cdrGroupCompanyId,
                    Code = "CDR",
                    NameEn = "CDR Group",
                    NameAr = "مجموعة سي دي آر",
                    TitleEn = "Creative Development & Realization | Strategic Production | Brand Growth",
                    TitleAr = "التطوير الإبداعي والتنفيذ | الإنتاج الاستراتيجي | نمو العلامة التجارية",
                    DescriptionEn = "We specialize in Creative Development & Realization, Strategic Production, and Brand Growth, transforming ideas into impactful realities. From concept creation to full-scale execution, we design strategic solutions that elevate brands, strengthen market presence, and drive sustainable growth. Our approach blends creativity with precision, ensuring every project delivers measurable results and long-term value.",
                    DescriptionAr = "نحن متخصصون في التطوير الإبداعي والتنفيذ، والإنتاج الاستراتيجي، ونمو العلامة التجارية، حيث نحوّل الأفكار إلى واقع مؤثر. من ابتكار المفاهيم إلى التنفيذ الشامل، نصمم حلولاً استراتيجية ترتقي بالعلامات التجارية وتعزز الحضور في السوق وتدفع النمو المستدام. يجمع نهجنا بين الإبداع والدقة، مما يضمن تحقيق نتائج قابلة للقياس وقيمة طويلة الأمد.",
                    StoryEn = "CDR Group has been a trusted partner in creative development, strategic production, and brand growth, delivering innovative solutions that drive results.",
                    StoryAr = "كانت مجموعة سي دي آر شريكاً موثوقاً في التطوير الإبداعي والإنتاج الاستراتيجي ونمو العلامة التجارية، حيث تقدم حلولاً مبتكرة تحقق النتائج.",
                    MissionEn = "To deliver innovative creative solutions that empower brands to grow and succeed in a competitive market.",
                    MissionAr = "تقديم حلول إبداعية مبتكرة تمكّن العلامات التجارية من النمو والنجاح في سوق تنافسي.",
                    VisionEn = "To be the leading creative agency in the region, recognized for excellence in development, realization, and strategic production.",
                    VisionAr = "أن نكون الوكالة الإبداعية الرائدة في المنطقة، والمعروفة بالتميز في التطوير والتنفيذ والإنتاج الاستراتيجي.",
                    PrimaryColor = "#D9A93E",
                    SecondaryColor = "#3E423D",
                    OpeningStartDay = "Sunday",
                    OpeningEndDay = "Thursday",
                    OpeningStartTime = new TimeSpan(9, 0, 0),   // 9:00 AM
                    OpeningEndTime = new TimeSpan(17, 0, 0),    // 5:00 PM
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
            );

        }
    }
}
