using Microsoft.EntityFrameworkCore;
using cdr_group.Domain.Entities;
using cdr_group.Domain.Entities.Identity;
using cdr_group.Domain.Constants;
using cdr_group.Domain.Enums;
using PermissionConstants = cdr_group.Domain.Constants.Permissions;

namespace cdr_group.Persistence.Data
{
    public partial class ApplicationDbContext
    {
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

            // Country Permission IDs
            var countriesReadId = Guid.Parse("aabbccdd-6666-6666-6666-aaaaaaaaaaaa");
            var countriesCreateId = Guid.Parse("aabbccdd-6666-6666-6666-bbbbbbbbbbbb");
            var countriesUpdateId = Guid.Parse("aabbccdd-6666-6666-6666-cccccccccccc");
            var countriesDeleteId = Guid.Parse("aabbccdd-6666-6666-6666-dddddddddddd");

            // City Permission IDs
            var citiesReadId = Guid.Parse("aabbccdd-7777-7777-7777-aaaaaaaaaaaa");
            var citiesCreateId = Guid.Parse("aabbccdd-7777-7777-7777-bbbbbbbbbbbb");
            var citiesUpdateId = Guid.Parse("aabbccdd-7777-7777-7777-cccccccccccc");
            var citiesDeleteId = Guid.Parse("aabbccdd-7777-7777-7777-dddddddddddd");

            // Partner Permission IDs
            var partnersReadId = Guid.Parse("aabbccdd-8888-8888-8888-aaaaaaaaaaaa");
            var partnersCreateId = Guid.Parse("aabbccdd-8888-8888-8888-bbbbbbbbbbbb");
            var partnersUpdateId = Guid.Parse("aabbccdd-8888-8888-8888-cccccccccccc");
            var partnersDeleteId = Guid.Parse("aabbccdd-8888-8888-8888-dddddddddddd");

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
                // Country permissions
                new Permission { Id = countriesReadId, Name = PermissionConstants.Countries.Read, Description = "View countries", Module = "Countries", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = countriesCreateId, Name = PermissionConstants.Countries.Create, Description = "Create countries", Module = "Countries", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = countriesUpdateId, Name = PermissionConstants.Countries.Update, Description = "Update countries", Module = "Countries", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = countriesDeleteId, Name = PermissionConstants.Countries.Delete, Description = "Delete countries", Module = "Countries", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // City permissions
                new Permission { Id = citiesReadId, Name = PermissionConstants.Cities.Read, Description = "View cities", Module = "Cities", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = citiesCreateId, Name = PermissionConstants.Cities.Create, Description = "Create cities", Module = "Cities", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = citiesUpdateId, Name = PermissionConstants.Cities.Update, Description = "Update cities", Module = "Cities", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = citiesDeleteId, Name = PermissionConstants.Cities.Delete, Description = "Delete cities", Module = "Cities", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // Partner permissions
                new Permission { Id = partnersReadId, Name = PermissionConstants.Partners.Read, Description = "View partners", Module = "Partners", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = partnersCreateId, Name = PermissionConstants.Partners.Create, Description = "Create partners", Module = "Partners", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = partnersUpdateId, Name = PermissionConstants.Partners.Update, Description = "Update partners", Module = "Partners", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = partnersDeleteId, Name = PermissionConstants.Partners.Delete, Description = "Delete partners", Module = "Partners", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
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
                    OpeningStartDay = "Saturday",
                    OpeningEndDay = "Friday",
                    OpeningStartTime = new TimeSpan(11, 0, 0),
                    OpeningEndTime = new TimeSpan(20, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                // Qubtan
                new Company
                {
                    Id = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"),
                    Code = "QBT",
                    NameEn = "Qubtan",
                    NameAr = "قبطان",
                    OpeningStartDay = "Saturday",
                    OpeningEndDay = "Friday",
                    OpeningStartTime = new TimeSpan(9, 0, 0),
                    OpeningEndTime = new TimeSpan(18, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                // Ghmas Baladi Red (child of Qubtan)
                new Company
                {
                    Id = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"),
                    Code = "GBR",
                    NameEn = "Ghmas Baladi Red",
                    NameAr = "غمس بلدي أحمر",
                    PrimaryColor = "#FD3E48",
                    SecondaryColor = "#461F1A",
                    OpeningStartDay = "Saturday",
                    OpeningEndDay = "Friday",
                    OpeningStartTime = new TimeSpan(9, 0, 0),
                    OpeningEndTime = new TimeSpan(18, 0, 0),
                    IsActive = true,
                    ParentId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                // Ghmas Baladi Yellow (child of Qubtan)
                new Company
                {
                    Id = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"),
                    Code = "GBY",
                    NameEn = "Ghmas Baladi Yellow",
                    NameAr = "غمس بلدي أصفر",
                    PrimaryColor = "#F7941D",
                    SecondaryColor = "#080E42",
                    OpeningStartDay = "Saturday",
                    OpeningEndDay = "Friday",
                    OpeningStartTime = new TimeSpan(9, 0, 0),
                    OpeningEndTime = new TimeSpan(18, 0, 0),
                    IsActive = true,
                    ParentId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                // Al Quds View (child of Qubtan)
                new Company
                {
                    Id = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"),
                    Code = "AQV",
                    NameEn = "Al Quds View",
                    NameAr = "طلة القدس",
                    PrimaryColor = "#B7442E",
                    SecondaryColor = "#C18C5E",
                    OpeningStartDay = "Saturday",
                    OpeningEndDay = "Friday",
                    OpeningStartTime = new TimeSpan(9, 0, 0),
                    OpeningEndTime = new TimeSpan(18, 0, 0),
                    IsActive = true,
                    ParentId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"),
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                // Sharakeh++
                new Company
                {
                    Id = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"),
                    Code = "SHR",
                    NameEn = "Sharakeh++",
                    NameAr = "شراكة++",
                    PrimaryColor= "#07B3DE",
                    SecondaryColor = "#080E42",
                    OpeningStartDay = "Saturday",
                    OpeningEndDay = "Friday",
                    OpeningStartTime = new TimeSpan(9, 0, 0),
                    OpeningEndTime = new TimeSpan(0, 0, 0),
                    PartnershipFormUrl = "https://docs.google.com/forms/d/e/1FAIpQLSf9gG6rlpDK_sBNbQ-AAxThIGMz61WIdQWEimtY9oc9AMcQFg/viewform?usp=header",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                },
                // Cinema Reels
                new Company
                {
                    Id = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"),
                    Code = "CNR",
                    NameEn = "Cinema Reels",
                    NameAr = "سينما ريلز",
                    PrimaryColor = "#FE662F",
                    SecondaryColor = "#B52EA5",
                    OpeningStartDay = "Saturday",
                    OpeningEndDay = "Friday",
                    OpeningStartTime = new TimeSpan(11, 0, 0),
                    OpeningEndTime = new TimeSpan(20, 0, 0),
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsDeleted = false
                }
            );

            // Seed countries
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000001"), NameEn = "Jordan", NameAr = "الأردن", Latitude = 31.95, Longitude = 35.93, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000002"), NameEn = "Saudi Arabia", NameAr = "المملكة العربية السعودية", Latitude = 23.88, Longitude = 45.08, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000003"), NameEn = "UAE", NameAr = "الإمارات العربية المتحدة", Latitude = 23.42, Longitude = 53.85, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000004"), NameEn = "Qatar", NameAr = "قطر", Latitude = 25.35, Longitude = 51.18, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000005"), NameEn = "Bahrain", NameAr = "البحرين", Latitude = 26.07, Longitude = 50.55, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000006"), NameEn = "Kuwait", NameAr = "الكويت", Latitude = 29.31, Longitude = 47.48, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000007"), NameEn = "Oman", NameAr = "عُمان", Latitude = 21.47, Longitude = 55.98, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000008"), NameEn = "Yemen", NameAr = "اليمن", Latitude = 15.55, Longitude = 48.52, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000009"), NameEn = "Iraq", NameAr = "العراق", Latitude = 33.22, Longitude = 43.68, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000010"), NameEn = "Syria", NameAr = "سوريا", Latitude = 34.80, Longitude = 38.99, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000011"), NameEn = "Lebanon", NameAr = "لبنان", Latitude = 33.85, Longitude = 35.86, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000012"), NameEn = "Palestine", NameAr = "فلسطين", Latitude = 31.95, Longitude = 35.23, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000013"), NameEn = "Egypt", NameAr = "مصر", Latitude = 26.82, Longitude = 30.80, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000014"), NameEn = "Turkey", NameAr = "تركيا", Latitude = 38.96, Longitude = 35.24, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000015"), NameEn = "Iran", NameAr = "إيران", Latitude = 32.43, Longitude = 53.69, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000016"), NameEn = "Libya", NameAr = "ليبيا", Latitude = 26.34, Longitude = 17.23, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000017"), NameEn = "Sudan", NameAr = "السودان", Latitude = 12.86, Longitude = 30.22, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000018"), NameEn = "Tunisia", NameAr = "تونس", Latitude = 33.89, Longitude = 9.54, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000019"), NameEn = "Algeria", NameAr = "الجزائر", Latitude = 28.03, Longitude = 1.66, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000020"), NameEn = "Morocco", NameAr = "المغرب", Latitude = 31.79, Longitude = -7.09, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000021"), NameEn = "Mauritania", NameAr = "موريتانيا", Latitude = 21.01, Longitude = -10.94, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000022"), NameEn = "Somalia", NameAr = "الصومال", Latitude = 5.15, Longitude = 46.20, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000023"), NameEn = "Djibouti", NameAr = "جيبوتي", Latitude = 11.83, Longitude = 42.59, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000024"), NameEn = "Comoros", NameAr = "جزر القمر", Latitude = -11.88, Longitude = 43.87, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false }
            );

            // Seed cities
            var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<City>().HasData(
                // Jordan
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000001"), NameEn = "Amman", NameAr = "عمّان", Latitude = 31.95, Longitude = 35.93, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000002"), NameEn = "Irbid", NameAr = "إربد", Latitude = 32.56, Longitude = 35.85, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000003"), NameEn = "Zarqa", NameAr = "الزرقاء", Latitude = 32.07, Longitude = 36.09, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000004"), NameEn = "Aqaba", NameAr = "العقبة", Latitude = 29.53, Longitude = 35.01, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000005"), NameEn = "Jarash", NameAr = "جرش", Latitude = 32.27, Longitude = 35.90, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000006"), NameEn = "Madaba", NameAr = "مادبا", Latitude = 31.72, Longitude = 35.79, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000007"), NameEn = "Salt", NameAr = "السلط", Latitude = 32.04, Longitude = 35.73, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000008"), NameEn = "Mafraq", NameAr = "المفرق", Latitude = 32.34, Longitude = 36.21, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000009"), NameEn = "Karak", NameAr = "الكرك", Latitude = 31.18, Longitude = 35.70, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000010"), NameEn = "Tafilah", NameAr = "الطفيلة", Latitude = 30.84, Longitude = 35.60, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000011"), NameEn = "Ma'an", NameAr = "معان", Latitude = 30.20, Longitude = 35.73, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000012"), NameEn = "Ajloun", NameAr = "عجلون", Latitude = 32.33, Longitude = 35.75, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000001"), CreatedAt = seedDate, IsDeleted = false },
                // Saudi Arabia
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000013"), NameEn = "Riyadh", NameAr = "الرياض", Latitude = 24.71, Longitude = 46.68, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000002"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000014"), NameEn = "Jeddah", NameAr = "جدة", Latitude = 21.49, Longitude = 39.19, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000002"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000015"), NameEn = "Mecca", NameAr = "مكة المكرمة", Latitude = 21.39, Longitude = 39.86, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000002"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000016"), NameEn = "Medina", NameAr = "المدينة المنورة", Latitude = 24.47, Longitude = 39.61, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000002"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000017"), NameEn = "Dammam", NameAr = "الدمام", Latitude = 26.43, Longitude = 50.10, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000002"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000018"), NameEn = "Tabuk", NameAr = "تبوك", Latitude = 28.38, Longitude = 36.57, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000002"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000019"), NameEn = "Abha", NameAr = "أبها", Latitude = 18.22, Longitude = 42.50, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000002"), CreatedAt = seedDate, IsDeleted = false },
                // UAE
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000020"), NameEn = "Dubai", NameAr = "دبي", Latitude = 25.20, Longitude = 55.27, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000003"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000021"), NameEn = "Abu Dhabi", NameAr = "أبو ظبي", Latitude = 24.45, Longitude = 54.37, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000003"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000022"), NameEn = "Sharjah", NameAr = "الشارقة", Latitude = 25.35, Longitude = 55.39, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000003"), CreatedAt = seedDate, IsDeleted = false },
                // Qatar
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000023"), NameEn = "Doha", NameAr = "الدوحة", Latitude = 25.29, Longitude = 51.53, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000004"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000024"), NameEn = "Al Wakrah", NameAr = "الوكرة", Latitude = 25.17, Longitude = 51.60, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000004"), CreatedAt = seedDate, IsDeleted = false },
                // Bahrain
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000025"), NameEn = "Manama", NameAr = "المنامة", Latitude = 26.23, Longitude = 50.59, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000005"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000026"), NameEn = "Muharraq", NameAr = "المحرق", Latitude = 26.26, Longitude = 50.62, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000005"), CreatedAt = seedDate, IsDeleted = false },
                // Kuwait
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000027"), NameEn = "Kuwait City", NameAr = "مدينة الكويت", Latitude = 29.38, Longitude = 47.99, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000006"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000028"), NameEn = "Hawalli", NameAr = "حولي", Latitude = 29.33, Longitude = 48.03, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000006"), CreatedAt = seedDate, IsDeleted = false },
                // Oman
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000029"), NameEn = "Muscat", NameAr = "مسقط", Latitude = 23.59, Longitude = 58.55, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000007"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000030"), NameEn = "Salalah", NameAr = "صلالة", Latitude = 17.02, Longitude = 54.09, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000007"), CreatedAt = seedDate, IsDeleted = false },
                // Yemen
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000031"), NameEn = "Sana'a", NameAr = "صنعاء", Latitude = 15.37, Longitude = 44.21, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000008"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000032"), NameEn = "Aden", NameAr = "عدن", Latitude = 12.79, Longitude = 45.02, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000008"), CreatedAt = seedDate, IsDeleted = false },
                // Iraq
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000033"), NameEn = "Baghdad", NameAr = "بغداد", Latitude = 33.31, Longitude = 44.37, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000009"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000034"), NameEn = "Basra", NameAr = "البصرة", Latitude = 30.51, Longitude = 47.81, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000009"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000035"), NameEn = "Erbil", NameAr = "أربيل", Latitude = 36.19, Longitude = 44.01, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000009"), CreatedAt = seedDate, IsDeleted = false },
                // Syria
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000036"), NameEn = "Damascus", NameAr = "دمشق", Latitude = 33.51, Longitude = 36.29, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000010"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000037"), NameEn = "Aleppo", NameAr = "حلب", Latitude = 36.20, Longitude = 37.16, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000010"), CreatedAt = seedDate, IsDeleted = false },
                // Lebanon
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000038"), NameEn = "Beirut", NameAr = "بيروت", Latitude = 33.89, Longitude = 35.50, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000011"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000039"), NameEn = "Tripoli", NameAr = "طرابلس", Latitude = 34.44, Longitude = 35.83, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000011"), CreatedAt = seedDate, IsDeleted = false },
                // Palestine
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000040"), NameEn = "Jerusalem", NameAr = "القدس", Latitude = 31.77, Longitude = 35.23, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000012"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000041"), NameEn = "Gaza", NameAr = "غزة", Latitude = 31.50, Longitude = 34.47, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000012"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000042"), NameEn = "Ramallah", NameAr = "رام الله", Latitude = 31.90, Longitude = 35.20, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000012"), CreatedAt = seedDate, IsDeleted = false },
                // Egypt
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000043"), NameEn = "Cairo", NameAr = "القاهرة", Latitude = 30.04, Longitude = 31.24, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000013"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000044"), NameEn = "Alexandria", NameAr = "الإسكندرية", Latitude = 31.20, Longitude = 29.92, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000013"), CreatedAt = seedDate, IsDeleted = false },
                // Turkey
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000045"), NameEn = "Ankara", NameAr = "أنقرة", Latitude = 39.93, Longitude = 32.85, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000014"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000046"), NameEn = "Istanbul", NameAr = "إسطنبول", Latitude = 41.01, Longitude = 28.98, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000014"), CreatedAt = seedDate, IsDeleted = false },
                // Iran
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000047"), NameEn = "Tehran", NameAr = "طهران", Latitude = 35.69, Longitude = 51.39, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000015"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000048"), NameEn = "Isfahan", NameAr = "أصفهان", Latitude = 32.65, Longitude = 51.68, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000015"), CreatedAt = seedDate, IsDeleted = false },
                // Libya
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000049"), NameEn = "Tripoli", NameAr = "طرابلس", Latitude = 32.90, Longitude = 13.18, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000016"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000050"), NameEn = "Benghazi", NameAr = "بنغازي", Latitude = 32.12, Longitude = 20.07, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000016"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000051"), NameEn = "Misrata", NameAr = "مصراتة", Latitude = 32.38, Longitude = 15.09, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000016"), CreatedAt = seedDate, IsDeleted = false },
                // Sudan
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000052"), NameEn = "Khartoum", NameAr = "الخرطوم", Latitude = 15.50, Longitude = 32.56, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000017"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000053"), NameEn = "Omdurman", NameAr = "أم درمان", Latitude = 15.64, Longitude = 32.48, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000017"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000054"), NameEn = "Port Sudan", NameAr = "بورتسودان", Latitude = 19.62, Longitude = 37.22, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000017"), CreatedAt = seedDate, IsDeleted = false },
                // Tunisia
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000055"), NameEn = "Tunis", NameAr = "تونس", Latitude = 36.81, Longitude = 10.17, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000018"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000056"), NameEn = "Sfax", NameAr = "صفاقس", Latitude = 34.74, Longitude = 10.76, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000018"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000057"), NameEn = "Sousse", NameAr = "سوسة", Latitude = 35.83, Longitude = 10.59, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000018"), CreatedAt = seedDate, IsDeleted = false },
                // Algeria
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000058"), NameEn = "Algiers", NameAr = "الجزائر", Latitude = 36.75, Longitude = 3.04, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000019"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000059"), NameEn = "Oran", NameAr = "وهران", Latitude = 35.70, Longitude = -0.63, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000019"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000060"), NameEn = "Constantine", NameAr = "قسنطينة", Latitude = 36.37, Longitude = 6.61, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000019"), CreatedAt = seedDate, IsDeleted = false },
                // Morocco
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000061"), NameEn = "Rabat", NameAr = "الرباط", Latitude = 34.02, Longitude = -6.84, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000020"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000062"), NameEn = "Casablanca", NameAr = "الدار البيضاء", Latitude = 33.57, Longitude = -7.59, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000020"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000063"), NameEn = "Marrakech", NameAr = "مراكش", Latitude = 31.63, Longitude = -8.01, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000020"), CreatedAt = seedDate, IsDeleted = false },
                // Mauritania
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000064"), NameEn = "Nouakchott", NameAr = "نواكشوط", Latitude = 18.09, Longitude = -15.98, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000021"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000065"), NameEn = "Nouadhibou", NameAr = "نواذيبو", Latitude = 20.94, Longitude = -17.04, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000021"), CreatedAt = seedDate, IsDeleted = false },
                // Somalia
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000066"), NameEn = "Mogadishu", NameAr = "مقديشو", Latitude = 2.05, Longitude = 45.32, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000022"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000067"), NameEn = "Hargeisa", NameAr = "هرجيسا", Latitude = 9.56, Longitude = 44.06, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000022"), CreatedAt = seedDate, IsDeleted = false },
                // Djibouti
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000068"), NameEn = "Djibouti City", NameAr = "مدينة جيبوتي", Latitude = 11.59, Longitude = 43.15, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000023"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000069"), NameEn = "Ali Sabieh", NameAr = "علي صبيح", Latitude = 11.16, Longitude = 42.71, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000023"), CreatedAt = seedDate, IsDeleted = false },
                // Comoros
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000070"), NameEn = "Moroni", NameAr = "موروني", Latitude = -11.70, Longitude = 43.26, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000024"), CreatedAt = seedDate, IsDeleted = false },
                new City { Id = Guid.Parse("D0000000-0000-0000-0000-000000000071"), NameEn = "Mutsamudu", NameAr = "موتسامودو", Latitude = -12.17, Longitude = 44.40, CountryId = Guid.Parse("C0000000-0000-0000-0000-000000000024"), CreatedAt = seedDate, IsDeleted = false }
            );

            // Seed partners for CDR
            modelBuilder.Entity<Partner>().HasData(
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000001"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000013"), Status = PartnerStatus.Present, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000002"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000033"), Status = PartnerStatus.Present, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000003"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000043"), Status = PartnerStatus.Present, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000004"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000001"), Status = PartnerStatus.Present, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Jordan (remaining cities)
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000005"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000002"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000006"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000003"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000007"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000004"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000008"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000005"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000009"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000006"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000010"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000007"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000011"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000008"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000012"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000009"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000013"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000010"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000014"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000011"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000015"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000012"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Saudi Arabia (remaining cities)
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000016"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000014"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000017"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000015"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000018"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000016"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000019"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000017"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000020"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000018"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000021"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000019"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - UAE
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000022"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000020"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000023"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000021"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000024"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000022"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Qatar
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000025"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000023"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000026"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000024"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Bahrain
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000027"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000025"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000028"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000026"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Kuwait
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000029"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000027"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000030"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000028"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Oman
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000031"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000029"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000032"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000030"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Yemen
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000033"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000031"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000034"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000032"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Iraq (remaining cities)
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000035"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000034"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000036"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000035"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Syria
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000037"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000036"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000038"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000037"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Lebanon
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000039"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000038"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000040"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000039"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Palestine
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000041"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000040"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000042"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000041"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000043"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000042"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Egypt (remaining)
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000044"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000044"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Turkey
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000045"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000045"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000046"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000046"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Iran
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000047"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000047"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000048"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000048"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Libya
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000049"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000049"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000050"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000050"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000051"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000051"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Sudan
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000052"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000052"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000053"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000053"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000054"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000054"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Tunisia
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000055"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000055"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000056"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000056"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000057"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000057"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Algeria
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000058"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000058"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000059"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000059"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000060"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000060"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Morocco
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000061"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000061"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000062"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000062"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000063"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000063"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Mauritania
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000064"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000064"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000065"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000065"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Somalia
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000066"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000066"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000067"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000067"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Djibouti
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000068"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000068"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000069"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000069"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                // Available partners - Comoros
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000070"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000070"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000071"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000071"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false }
            );

        }
    }
}
