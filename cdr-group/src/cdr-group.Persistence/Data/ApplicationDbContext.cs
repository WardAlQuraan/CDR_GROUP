using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Entities.Base;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Contracts.Interfaces.Services;
using cdr_group.Domain.Constants;

namespace cdr_group.Persistence.Data
{
    public partial class ApplicationDbContext : DbContext
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
            var currentTime = DateTime.Now;
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
                bool b => b ? "Yes" : "No",
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
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Partner> Partners { get; set; }

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
                entity.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
                entity.Property(e => e.NameAr).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DescriptionEn).HasMaxLength(2000);
                entity.Property(e => e.DescriptionAr).HasMaxLength(2000);
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

            // Country configuration
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
                entity.Property(e => e.NameAr).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Latitude).IsRequired();
                entity.Property(e => e.Longitude).IsRequired();
            });

            // City configuration
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NameEn).IsRequired().HasMaxLength(200);
                entity.Property(e => e.NameAr).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Latitude).IsRequired();
                entity.Property(e => e.Longitude).IsRequired();

                entity.HasOne(e => e.Country)
                    .WithMany(c => c.Cities)
                    .HasForeignKey(e => e.CountryId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Partner configuration
            modelBuilder.Entity<Partner>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(50);

                entity.HasOne(e => e.Company)
                    .WithMany()
                    .HasForeignKey(e => e.CompanyId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.City)
                    .WithMany()
                    .HasForeignKey(e => e.CityId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed default data
            SeedData(modelBuilder);
        }

    }
}
