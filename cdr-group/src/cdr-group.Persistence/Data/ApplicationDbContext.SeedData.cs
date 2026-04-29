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

            // CompanyBackground Permission IDs
            var companyBackgroundsReadId = Guid.Parse("aabbccdd-9999-9999-9999-aaaaaaaaaaaa");
            var companyBackgroundsCreateId = Guid.Parse("aabbccdd-9999-9999-9999-bbbbbbbbbbbb");
            var companyBackgroundsUpdateId = Guid.Parse("aabbccdd-9999-9999-9999-cccccccccccc");
            var companyBackgroundsDeleteId = Guid.Parse("aabbccdd-9999-9999-9999-dddddddddddd");

            // CompanyForm Permission IDs
            var companyFormsReadId = Guid.Parse("bbccddee-1111-1111-1111-aaaaaaaaaaaa");
            var companyFormsCreateId = Guid.Parse("bbccddee-1111-1111-1111-bbbbbbbbbbbb");
            var companyFormsUpdateId = Guid.Parse("bbccddee-1111-1111-1111-cccccccccccc");
            var companyFormsDeleteId = Guid.Parse("bbccddee-1111-1111-1111-dddddddddddd");

            // CompanyPreference Permission IDs
            var companyPreferencesReadId = Guid.Parse("bbccddee-2222-2222-2222-aaaaaaaaaaaa");
            var companyPreferencesCreateId = Guid.Parse("bbccddee-2222-2222-2222-bbbbbbbbbbbb");
            var companyPreferencesUpdateId = Guid.Parse("bbccddee-2222-2222-2222-cccccccccccc");
            var companyPreferencesDeleteId = Guid.Parse("bbccddee-2222-2222-2222-dddddddddddd");

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
                // CompanyBackground permissions
                new Permission { Id = companyBackgroundsReadId, Name = PermissionConstants.CompanyBackgrounds.Read, Description = "View company backgrounds", Module = "CompanyBackgrounds", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyBackgroundsCreateId, Name = PermissionConstants.CompanyBackgrounds.Create, Description = "Create company backgrounds", Module = "CompanyBackgrounds", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyBackgroundsUpdateId, Name = PermissionConstants.CompanyBackgrounds.Update, Description = "Update company backgrounds", Module = "CompanyBackgrounds", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyBackgroundsDeleteId, Name = PermissionConstants.CompanyBackgrounds.Delete, Description = "Delete company backgrounds", Module = "CompanyBackgrounds", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // CompanyForm permissions
                new Permission { Id = companyFormsReadId, Name = PermissionConstants.CompanyForms.Read, Description = "View company forms", Module = "CompanyForms", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyFormsCreateId, Name = PermissionConstants.CompanyForms.Create, Description = "Create company forms", Module = "CompanyForms", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyFormsUpdateId, Name = PermissionConstants.CompanyForms.Update, Description = "Update company forms", Module = "CompanyForms", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyFormsDeleteId, Name = PermissionConstants.CompanyForms.Delete, Description = "Delete company forms", Module = "CompanyForms", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                // CompanyPreference permissions
                new Permission { Id = companyPreferencesReadId, Name = PermissionConstants.CompanyPreferences.Read, Description = "View company preferences", Module = "CompanyPreferences", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyPreferencesCreateId, Name = PermissionConstants.CompanyPreferences.Create, Description = "Create company preferences", Module = "CompanyPreferences", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyPreferencesUpdateId, Name = PermissionConstants.CompanyPreferences.Update, Description = "Update company preferences", Module = "CompanyPreferences", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Permission { Id = companyPreferencesDeleteId, Name = PermissionConstants.CompanyPreferences.Delete, Description = "Delete company preferences", Module = "CompanyPreferences", CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
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
                    NumberOfEmployees = 500,
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
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000024"), NameEn = "Comoros", NameAr = "جزر القمر", Latitude = -11.88, Longitude = 43.87, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000025"), NameEn = "Albania", NameAr = "ألبانيا", Latitude = 41.15, Longitude = 20.17, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000026"), NameEn = "Andorra", NameAr = "أندورا", Latitude = 42.55, Longitude = 1.52, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000027"), NameEn = "Austria", NameAr = "النمسا", Latitude = 47.52, Longitude = 14.55, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000028"), NameEn = "Belarus", NameAr = "بيلاروسيا", Latitude = 53.71, Longitude = 27.95, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000029"), NameEn = "Belgium", NameAr = "بلجيكا", Latitude = 50.85, Longitude = 4.35, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000030"), NameEn = "Bosnia and Herzegovina", NameAr = "البوسنة والهرسك", Latitude = 43.92, Longitude = 17.68, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000031"), NameEn = "Bulgaria", NameAr = "بلغاريا", Latitude = 42.73, Longitude = 25.49, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000032"), NameEn = "Croatia", NameAr = "كرواتيا", Latitude = 45.10, Longitude = 15.20, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000033"), NameEn = "Cyprus", NameAr = "قبرص", Latitude = 35.13, Longitude = 33.43, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000034"), NameEn = "Czech Republic", NameAr = "التشيك", Latitude = 49.82, Longitude = 15.47, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000035"), NameEn = "Denmark", NameAr = "الدنمارك", Latitude = 56.26, Longitude = 9.50, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000036"), NameEn = "Estonia", NameAr = "إستونيا", Latitude = 58.60, Longitude = 25.01, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000037"), NameEn = "Finland", NameAr = "فنلندا", Latitude = 61.92, Longitude = 25.75, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000038"), NameEn = "France", NameAr = "فرنسا", Latitude = 46.23, Longitude = 2.21, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000039"), NameEn = "Germany", NameAr = "ألمانيا", Latitude = 51.17, Longitude = 10.45, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000040"), NameEn = "Greece", NameAr = "اليونان", Latitude = 39.07, Longitude = 21.82, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000041"), NameEn = "Hungary", NameAr = "المجر", Latitude = 47.16, Longitude = 19.50, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000042"), NameEn = "Iceland", NameAr = "آيسلندا", Latitude = 64.96, Longitude = -19.02, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000043"), NameEn = "Ireland", NameAr = "أيرلندا", Latitude = 53.14, Longitude = -7.69, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000044"), NameEn = "Italy", NameAr = "إيطاليا", Latitude = 41.87, Longitude = 12.57, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000045"), NameEn = "Kosovo", NameAr = "كوسوفو", Latitude = 42.60, Longitude = 20.90, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000046"), NameEn = "Latvia", NameAr = "لاتفيا", Latitude = 56.88, Longitude = 24.60, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000047"), NameEn = "Liechtenstein", NameAr = "ليختنشتاين", Latitude = 47.17, Longitude = 9.56, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000048"), NameEn = "Lithuania", NameAr = "ليتوانيا", Latitude = 55.17, Longitude = 23.88, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000049"), NameEn = "Luxembourg", NameAr = "لوكسمبورغ", Latitude = 49.82, Longitude = 6.13, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000050"), NameEn = "Malta", NameAr = "مالطا", Latitude = 35.94, Longitude = 14.38, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000051"), NameEn = "Moldova", NameAr = "مولدوفا", Latitude = 47.41, Longitude = 28.37, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000052"), NameEn = "Monaco", NameAr = "موناكو", Latitude = 43.75, Longitude = 7.42, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000053"), NameEn = "Montenegro", NameAr = "الجبل الأسود", Latitude = 42.71, Longitude = 19.37, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000054"), NameEn = "Netherlands", NameAr = "هولندا", Latitude = 52.13, Longitude = 5.29, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000055"), NameEn = "North Macedonia", NameAr = "مقدونيا الشمالية", Latitude = 41.51, Longitude = 21.75, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000056"), NameEn = "Norway", NameAr = "النرويج", Latitude = 60.47, Longitude = 8.47, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000057"), NameEn = "Poland", NameAr = "بولندا", Latitude = 51.92, Longitude = 19.15, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000058"), NameEn = "Portugal", NameAr = "البرتغال", Latitude = 39.40, Longitude = -8.22, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000059"), NameEn = "Romania", NameAr = "رومانيا", Latitude = 45.94, Longitude = 24.97, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000060"), NameEn = "Russia", NameAr = "روسيا", Latitude = 61.52, Longitude = 105.32, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000061"), NameEn = "San Marino", NameAr = "سان مارينو", Latitude = 43.94, Longitude = 12.46, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000062"), NameEn = "Serbia", NameAr = "صربيا", Latitude = 44.02, Longitude = 21.01, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000063"), NameEn = "Slovakia", NameAr = "سلوفاكيا", Latitude = 48.67, Longitude = 19.70, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000064"), NameEn = "Slovenia", NameAr = "سلوفينيا", Latitude = 46.15, Longitude = 15.00, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000065"), NameEn = "Spain", NameAr = "إسبانيا", Latitude = 40.46, Longitude = -3.75, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000066"), NameEn = "Sweden", NameAr = "السويد", Latitude = 60.13, Longitude = 18.64, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000067"), NameEn = "Switzerland", NameAr = "سويسرا", Latitude = 46.82, Longitude = 8.23, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000068"), NameEn = "Ukraine", NameAr = "أوكرانيا", Latitude = 48.38, Longitude = 31.17, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000069"), NameEn = "United Kingdom", NameAr = "المملكة المتحدة", Latitude = 55.38, Longitude = -3.44, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000070"), NameEn = "Vatican City", NameAr = "الفاتيكان", Latitude = 41.90, Longitude = 12.45, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000071"), NameEn = "Afghanistan", NameAr = "أفغانستان", Latitude = 33.94, Longitude = 67.71, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000072"), NameEn = "Armenia", NameAr = "أرمينيا", Latitude = 40.07, Longitude = 45.04, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000073"), NameEn = "Azerbaijan", NameAr = "أذربيجان", Latitude = 40.14, Longitude = 47.58, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000074"), NameEn = "Bangladesh", NameAr = "بنغلاديش", Latitude = 23.68, Longitude = 90.36, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000075"), NameEn = "Bhutan", NameAr = "بوتان", Latitude = 27.51, Longitude = 90.43, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000076"), NameEn = "Brunei", NameAr = "بروناي", Latitude = 4.54, Longitude = 114.73, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000077"), NameEn = "Cambodia", NameAr = "كمبوديا", Latitude = 12.57, Longitude = 104.99, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000078"), NameEn = "China", NameAr = "الصين", Latitude = 35.86, Longitude = 104.20, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000079"), NameEn = "Georgia", NameAr = "جورجيا", Latitude = 42.32, Longitude = 43.36, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000080"), NameEn = "India", NameAr = "الهند", Latitude = 20.59, Longitude = 78.96, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000081"), NameEn = "Indonesia", NameAr = "إندونيسيا", Latitude = -0.79, Longitude = 113.92, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000082"), NameEn = "Japan", NameAr = "اليابان", Latitude = 36.20, Longitude = 138.25, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000083"), NameEn = "Kazakhstan", NameAr = "كازاخستان", Latitude = 48.02, Longitude = 66.92, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000084"), NameEn = "Kyrgyzstan", NameAr = "قيرغيزستان", Latitude = 41.20, Longitude = 74.77, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000085"), NameEn = "Laos", NameAr = "لاوس", Latitude = 19.86, Longitude = 102.50, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000086"), NameEn = "Malaysia", NameAr = "ماليزيا", Latitude = 4.21, Longitude = 101.98, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000087"), NameEn = "Maldives", NameAr = "المالديف", Latitude = 3.20, Longitude = 73.22, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000088"), NameEn = "Mongolia", NameAr = "منغوليا", Latitude = 46.86, Longitude = 103.85, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000089"), NameEn = "Myanmar", NameAr = "ميانمار", Latitude = 21.91, Longitude = 95.96, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000090"), NameEn = "Nepal", NameAr = "نيبال", Latitude = 28.39, Longitude = 84.12, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000091"), NameEn = "North Korea", NameAr = "كوريا الشمالية", Latitude = 40.34, Longitude = 127.51, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000092"), NameEn = "Pakistan", NameAr = "باكستان", Latitude = 30.38, Longitude = 69.35, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000093"), NameEn = "Philippines", NameAr = "الفلبين", Latitude = 12.88, Longitude = 121.77, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000094"), NameEn = "Singapore", NameAr = "سنغافورة", Latitude = 1.35, Longitude = 103.82, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000095"), NameEn = "South Korea", NameAr = "كوريا الجنوبية", Latitude = 35.91, Longitude = 127.77, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000096"), NameEn = "Sri Lanka", NameAr = "سريلانكا", Latitude = 7.87, Longitude = 80.77, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000097"), NameEn = "Tajikistan", NameAr = "طاجيكستان", Latitude = 38.86, Longitude = 71.28, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000098"), NameEn = "Thailand", NameAr = "تايلاند", Latitude = 15.87, Longitude = 100.99, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000099"), NameEn = "Timor-Leste", NameAr = "تيمور الشرقية", Latitude = -8.87, Longitude = 125.73, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000100"), NameEn = "Turkmenistan", NameAr = "تركمانستان", Latitude = 38.97, Longitude = 59.56, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000101"), NameEn = "Uzbekistan", NameAr = "أوزبكستان", Latitude = 41.38, Longitude = 64.59, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000102"), NameEn = "Vietnam", NameAr = "فيتنام", Latitude = 14.06, Longitude = 108.28, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000104"), NameEn = "Angola", NameAr = "أنغولا", Latitude = -11.20, Longitude = 17.87, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000105"), NameEn = "Benin", NameAr = "بنين", Latitude = 9.31, Longitude = 2.32, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000106"), NameEn = "Botswana", NameAr = "بوتسوانا", Latitude = -22.33, Longitude = 24.68, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000107"), NameEn = "Burkina Faso", NameAr = "بوركينا فاسو", Latitude = 12.24, Longitude = -1.56, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000108"), NameEn = "Burundi", NameAr = "بوروندي", Latitude = -3.37, Longitude = 29.92, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000109"), NameEn = "Cabo Verde", NameAr = "الرأس الأخضر", Latitude = 16.00, Longitude = -24.01, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000110"), NameEn = "Cameroon", NameAr = "الكاميرون", Latitude = 7.37, Longitude = 12.35, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000111"), NameEn = "Central African Republic", NameAr = "جمهورية أفريقيا الوسطى", Latitude = 6.61, Longitude = 20.94, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000112"), NameEn = "Chad", NameAr = "تشاد", Latitude = 15.45, Longitude = 18.73, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000113"), NameEn = "Congo (Brazzaville)", NameAr = "الكونغو", Latitude = -0.23, Longitude = 15.83, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000114"), NameEn = "Congo (Kinshasa)", NameAr = "الكونغو الديمقراطية", Latitude = -4.04, Longitude = 21.76, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000115"), NameEn = "Côte d'Ivoire", NameAr = "ساحل العاج", Latitude = 7.54, Longitude = -5.55, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000116"), NameEn = "Equatorial Guinea", NameAr = "غينيا الاستوائية", Latitude = 1.65, Longitude = 10.27, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000117"), NameEn = "Eritrea", NameAr = "إريتريا", Latitude = 15.18, Longitude = 39.78, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000118"), NameEn = "Eswatini", NameAr = "إسواتيني", Latitude = -26.52, Longitude = 31.47, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000119"), NameEn = "Ethiopia", NameAr = "إثيوبيا", Latitude = 9.15, Longitude = 40.49, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000120"), NameEn = "Gabon", NameAr = "الغابون", Latitude = -0.80, Longitude = 11.61, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000121"), NameEn = "Gambia", NameAr = "غامبيا", Latitude = 13.44, Longitude = -15.31, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000122"), NameEn = "Ghana", NameAr = "غانا", Latitude = 7.95, Longitude = -1.02, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000123"), NameEn = "Guinea", NameAr = "غينيا", Latitude = 9.95, Longitude = -9.70, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000124"), NameEn = "Guinea-Bissau", NameAr = "غينيا بيساو", Latitude = 11.80, Longitude = -15.18, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000125"), NameEn = "Kenya", NameAr = "كينيا", Latitude = -0.02, Longitude = 37.91, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000126"), NameEn = "Lesotho", NameAr = "ليسوتو", Latitude = -29.61, Longitude = 28.23, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000127"), NameEn = "Liberia", NameAr = "ليبيريا", Latitude = 6.43, Longitude = -9.43, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000128"), NameEn = "Madagascar", NameAr = "مدغشقر", Latitude = -18.77, Longitude = 46.87, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000129"), NameEn = "Malawi", NameAr = "مالاوي", Latitude = -13.25, Longitude = 34.30, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000130"), NameEn = "Mali", NameAr = "مالي", Latitude = 17.57, Longitude = -4.00, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000131"), NameEn = "Mauritius", NameAr = "موريشيوس", Latitude = -20.35, Longitude = 57.55, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000132"), NameEn = "Mozambique", NameAr = "موزمبيق", Latitude = -18.67, Longitude = 35.53, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000133"), NameEn = "Namibia", NameAr = "ناميبيا", Latitude = -22.96, Longitude = 18.49, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000134"), NameEn = "Niger", NameAr = "النيجر", Latitude = 17.61, Longitude = 8.08, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000135"), NameEn = "Nigeria", NameAr = "نيجيريا", Latitude = 9.08, Longitude = 8.68, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000136"), NameEn = "Rwanda", NameAr = "رواندا", Latitude = -1.94, Longitude = 29.87, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000137"), NameEn = "São Tomé and Príncipe", NameAr = "ساو تومي وبرينسيبي", Latitude = 0.19, Longitude = 6.61, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000138"), NameEn = "Senegal", NameAr = "السنغال", Latitude = 14.50, Longitude = -14.45, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000139"), NameEn = "Seychelles", NameAr = "سيشل", Latitude = -4.68, Longitude = 55.49, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000140"), NameEn = "Sierra Leone", NameAr = "سيراليون", Latitude = 8.46, Longitude = -11.78, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000141"), NameEn = "South Africa", NameAr = "جنوب أفريقيا", Latitude = -30.56, Longitude = 22.94, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000142"), NameEn = "South Sudan", NameAr = "جنوب السودان", Latitude = 6.88, Longitude = 31.31, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000143"), NameEn = "Tanzania", NameAr = "تنزانيا", Latitude = -6.37, Longitude = 34.89, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000144"), NameEn = "Togo", NameAr = "توغو", Latitude = 8.62, Longitude = 0.82, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000145"), NameEn = "Uganda", NameAr = "أوغندا", Latitude = 1.37, Longitude = 32.29, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000146"), NameEn = "Zambia", NameAr = "زامبيا", Latitude = -13.13, Longitude = 27.85, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000147"), NameEn = "Zimbabwe", NameAr = "زيمبابوي", Latitude = -19.02, Longitude = 29.15, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000148"), NameEn = "Antigua and Barbuda", NameAr = "أنتيغوا وباربودا", Latitude = 17.06, Longitude = -61.80, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000149"), NameEn = "Argentina", NameAr = "الأرجنتين", Latitude = -38.42, Longitude = -63.62, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000150"), NameEn = "Bahamas", NameAr = "الباهاماس", Latitude = 25.03, Longitude = -77.40, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000151"), NameEn = "Barbados", NameAr = "باربادوس", Latitude = 13.19, Longitude = -59.54, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000152"), NameEn = "Belize", NameAr = "بليز", Latitude = 17.19, Longitude = -88.50, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000153"), NameEn = "Bolivia", NameAr = "بوليفيا", Latitude = -16.29, Longitude = -63.59, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000154"), NameEn = "Brazil", NameAr = "البرازيل", Latitude = -14.24, Longitude = -51.93, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000155"), NameEn = "Canada", NameAr = "كندا", Latitude = 56.13, Longitude = -106.35, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000156"), NameEn = "Chile", NameAr = "تشيلي", Latitude = -35.68, Longitude = -71.54, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000157"), NameEn = "Colombia", NameAr = "كولومبيا", Latitude = 4.57, Longitude = -74.30, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000158"), NameEn = "Costa Rica", NameAr = "كوستاريكا", Latitude = 9.75, Longitude = -83.75, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000159"), NameEn = "Cuba", NameAr = "كوبا", Latitude = 21.52, Longitude = -77.78, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000160"), NameEn = "Dominica", NameAr = "دومينيكا", Latitude = 15.41, Longitude = -61.37, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000161"), NameEn = "Dominican Republic", NameAr = "جمهورية الدومينيكان", Latitude = 18.74, Longitude = -70.16, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000162"), NameEn = "Ecuador", NameAr = "الإكوادور", Latitude = -1.83, Longitude = -78.18, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000163"), NameEn = "El Salvador", NameAr = "السلفادور", Latitude = 13.79, Longitude = -88.90, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000164"), NameEn = "Grenada", NameAr = "غرينادا", Latitude = 12.26, Longitude = -61.60, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000165"), NameEn = "Guatemala", NameAr = "غواتيمالا", Latitude = 15.78, Longitude = -90.23, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000166"), NameEn = "Guyana", NameAr = "غيانا", Latitude = 4.86, Longitude = -58.93, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000167"), NameEn = "Haiti", NameAr = "هايتي", Latitude = 18.97, Longitude = -72.29, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000168"), NameEn = "Honduras", NameAr = "هندوراس", Latitude = 15.20, Longitude = -86.24, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000169"), NameEn = "Jamaica", NameAr = "جامايكا", Latitude = 18.11, Longitude = -77.30, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000170"), NameEn = "Mexico", NameAr = "المكسيك", Latitude = 23.63, Longitude = -102.55, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000171"), NameEn = "Nicaragua", NameAr = "نيكاراغوا", Latitude = 12.87, Longitude = -85.21, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000172"), NameEn = "Panama", NameAr = "بنما", Latitude = 8.54, Longitude = -80.78, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000173"), NameEn = "Paraguay", NameAr = "باراغواي", Latitude = -23.44, Longitude = -58.44, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000174"), NameEn = "Peru", NameAr = "بيرو", Latitude = -9.19, Longitude = -75.02, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000175"), NameEn = "Saint Kitts and Nevis", NameAr = "سانت كيتس ونيفيس", Latitude = 17.36, Longitude = -62.78, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000176"), NameEn = "Saint Lucia", NameAr = "سانت لوسيا", Latitude = 13.91, Longitude = -60.98, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000177"), NameEn = "Saint Vincent and the Grenadines", NameAr = "سانت فينسنت والغرينادين", Latitude = 12.98, Longitude = -61.29, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000178"), NameEn = "Suriname", NameAr = "سورينام", Latitude = 3.92, Longitude = -56.03, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000179"), NameEn = "Trinidad and Tobago", NameAr = "ترينيداد وتوباغو", Latitude = 10.69, Longitude = -61.22, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000180"), NameEn = "United States", NameAr = "الولايات المتحدة", Latitude = 37.09, Longitude = -95.71, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000181"), NameEn = "Uruguay", NameAr = "أوروغواي", Latitude = -32.52, Longitude = -55.77, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000182"), NameEn = "Venezuela", NameAr = "فنزويلا", Latitude = 6.42, Longitude = -66.59, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000183"), NameEn = "Australia", NameAr = "أستراليا", Latitude = -25.27, Longitude = 133.78, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000184"), NameEn = "Fiji", NameAr = "فيجي", Latitude = -17.71, Longitude = 178.07, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000185"), NameEn = "Kiribati", NameAr = "كيريباتي", Latitude = -3.37, Longitude = -168.73, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000186"), NameEn = "Marshall Islands", NameAr = "جزر مارشال", Latitude = 7.13, Longitude = 171.18, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000187"), NameEn = "Micronesia", NameAr = "ميكرونيزيا", Latitude = 7.43, Longitude = 150.55, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000188"), NameEn = "Nauru", NameAr = "ناورو", Latitude = -0.52, Longitude = 166.93, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000189"), NameEn = "New Zealand", NameAr = "نيوزيلندا", Latitude = -40.90, Longitude = 174.89, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000190"), NameEn = "Palau", NameAr = "بالاو", Latitude = 7.51, Longitude = 134.58, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000191"), NameEn = "Papua New Guinea", NameAr = "بابوا غينيا الجديدة", Latitude = -6.31, Longitude = 143.96, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000192"), NameEn = "Samoa", NameAr = "ساموا", Latitude = -13.76, Longitude = -172.10, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000193"), NameEn = "Solomon Islands", NameAr = "جزر سليمان", Latitude = -9.65, Longitude = 160.16, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000194"), NameEn = "Tonga", NameAr = "تونغا", Latitude = -21.18, Longitude = -175.20, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000195"), NameEn = "Tuvalu", NameAr = "توفالو", Latitude = -7.11, Longitude = 177.65, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000196"), NameEn = "Vanuatu", NameAr = "فانواتو", Latitude = -15.38, Longitude = 166.96, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
                new Country { Id = Guid.Parse("C0000000-0000-0000-0000-000000000197"), NameEn = "Taiwan", NameAr = "تايوان", Latitude = 23.70, Longitude = 120.96, CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false }
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
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0000-000000000071"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-aabbccddeeff"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000071"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },

                // ===== Qubtan Partners =====
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000001"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000001"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000002"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000002"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000003"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000003"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000004"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000004"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000005"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000005"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000006"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000006"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000007"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000007"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000008"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000008"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000009"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000009"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000010"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000010"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000011"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000011"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000012"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000012"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000013"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000013"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000014"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000014"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000015"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000015"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000016"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000016"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000017"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000017"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000018"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000018"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000019"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000019"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000020"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000020"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000021"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000021"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000022"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000022"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000023"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000023"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000024"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000024"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000025"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000025"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000026"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000026"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000027"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000027"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000028"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000028"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000029"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000029"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000030"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000030"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000031"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000031"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000032"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000032"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000033"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000033"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000034"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000034"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000035"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000035"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000036"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000036"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000037"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000037"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000038"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000038"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000039"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000039"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000040"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000040"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000041"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000041"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000042"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000042"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000043"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000043"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000044"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000044"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000045"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000045"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000046"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000046"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000047"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000047"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000048"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000048"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000049"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000049"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000050"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000050"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000051"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000051"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000052"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000052"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000053"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000053"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000054"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000054"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000055"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000055"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000056"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000056"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000057"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000057"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000058"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000058"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000059"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000059"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000060"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000060"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000061"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000061"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000062"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000062"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000063"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000063"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000064"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000064"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000065"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000065"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000066"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000066"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000067"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000067"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000068"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000068"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000069"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000069"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000070"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000070"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0001-000000000071"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000001"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000071"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },

                // ===== Ghmas Baladi Red Partners =====
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000001"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000001"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000002"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000002"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000003"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000003"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000004"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000004"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000005"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000005"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000006"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000006"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000007"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000007"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000008"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000008"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000009"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000009"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000010"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000010"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000011"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000011"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000012"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000012"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000013"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000013"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000014"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000014"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000015"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000015"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000016"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000016"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000017"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000017"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000018"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000018"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000019"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000019"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000020"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000020"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000021"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000021"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000022"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000022"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000023"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000023"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000024"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000024"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000025"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000025"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000026"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000026"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000027"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000027"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000028"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000028"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000029"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000029"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000030"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000030"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000031"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000031"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000032"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000032"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000033"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000033"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000034"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000034"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000035"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000035"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000036"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000036"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000037"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000037"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000038"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000038"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000039"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000039"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000040"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000040"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000041"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000041"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000042"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000042"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000043"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000043"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000044"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000044"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000045"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000045"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000046"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000046"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000047"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000047"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000048"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000048"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000049"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000049"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000050"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000050"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000051"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000051"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000052"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000052"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000053"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000053"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000054"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000054"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000055"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000055"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000056"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000056"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000057"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000057"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000058"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000058"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000059"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000059"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000060"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000060"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000061"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000061"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000062"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000062"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000063"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000063"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000064"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000064"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000065"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000065"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000066"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000066"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000067"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000067"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000068"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000068"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000069"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000069"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000070"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000070"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0002-000000000071"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000002"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000071"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },

                // ===== Ghmas Baladi Yellow Partners =====
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000001"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000001"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000002"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000002"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000003"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000003"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000004"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000004"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000005"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000005"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000006"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000006"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000007"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000007"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000008"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000008"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000009"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000009"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000010"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000010"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000011"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000011"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000012"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000012"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000013"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000013"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000014"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000014"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000015"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000015"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000016"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000016"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000017"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000017"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000018"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000018"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000019"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000019"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000020"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000020"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000021"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000021"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000022"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000022"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000023"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000023"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000024"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000024"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000025"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000025"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000026"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000026"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000027"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000027"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000028"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000028"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000029"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000029"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000030"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000030"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000031"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000031"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000032"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000032"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000033"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000033"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000034"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000034"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000035"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000035"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000036"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000036"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000037"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000037"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000038"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000038"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000039"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000039"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000040"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000040"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000041"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000041"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000042"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000042"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000043"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000043"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000044"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000044"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000045"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000045"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000046"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000046"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000047"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000047"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000048"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000048"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000049"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000049"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000050"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000050"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000051"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000051"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000052"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000052"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000053"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000053"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000054"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000054"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000055"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000055"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000056"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000056"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000057"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000057"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000058"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000058"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000059"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000059"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000060"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000060"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000061"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000061"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000062"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000062"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000063"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000063"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000064"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000064"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000065"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000065"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000066"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000066"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000067"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000067"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000068"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000068"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000069"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000069"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000070"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000070"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0003-000000000071"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000003"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000071"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },

                // ===== Al Quds View Partners =====
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000001"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000001"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000002"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000002"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000003"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000003"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000004"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000004"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000005"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000005"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000006"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000006"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000007"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000007"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000008"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000008"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000009"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000009"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000010"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000010"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000011"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000011"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000012"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000012"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000013"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000013"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000014"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000014"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000015"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000015"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000016"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000016"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000017"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000017"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000018"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000018"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000019"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000019"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000020"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000020"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000021"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000021"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000022"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000022"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000023"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000023"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000024"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000024"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000025"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000025"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000026"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000026"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000027"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000027"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000028"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000028"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000029"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000029"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000030"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000030"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000031"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000031"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000032"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000032"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000033"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000033"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000034"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000034"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000035"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000035"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000036"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000036"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000037"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000037"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000038"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000038"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000039"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000039"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000040"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000040"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000041"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000041"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000042"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000042"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000043"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000043"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000044"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000044"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000045"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000045"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000046"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000046"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000047"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000047"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000048"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000048"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000049"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000049"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000050"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000050"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000051"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000051"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000052"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000052"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000053"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000053"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000054"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000054"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000055"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000055"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000056"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000056"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000057"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000057"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000058"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000058"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000059"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000059"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000060"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000060"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000061"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000061"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000062"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000062"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000063"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000063"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000064"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000064"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000065"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000065"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000066"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000066"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000067"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000067"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000068"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000068"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000069"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000069"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000070"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000070"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0004-000000000071"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000004"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000071"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },

                // ===== Sharakeh++ Partners =====
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000001"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000001"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000002"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000002"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000003"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000003"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000004"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000004"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000005"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000005"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000006"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000006"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000007"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000007"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000008"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000008"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000009"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000009"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000010"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000010"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000011"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000011"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000012"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000012"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000013"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000013"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000014"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000014"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000015"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000015"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000016"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000016"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000017"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000017"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000018"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000018"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000019"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000019"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000020"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000020"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000021"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000021"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000022"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000022"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000023"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000023"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000024"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000024"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000025"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000025"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000026"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000026"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000027"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000027"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000028"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000028"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000029"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000029"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000030"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000030"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000031"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000031"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000032"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000032"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000033"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000033"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000034"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000034"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000035"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000035"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000036"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000036"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000037"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000037"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000038"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000038"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000039"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000039"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000040"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000040"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000041"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000041"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000042"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000042"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000043"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000043"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000044"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000044"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000045"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000045"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000046"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000046"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000047"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000047"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000048"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000048"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000049"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000049"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000050"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000050"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000051"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000051"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000052"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000052"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000053"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000053"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000054"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000054"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000055"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000055"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000056"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000056"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000057"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000057"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000058"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000058"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000059"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000059"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000060"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000060"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000061"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000061"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000062"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000062"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000063"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000063"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000064"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000064"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000065"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000065"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000066"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000066"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000067"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000067"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000068"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000068"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000069"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000069"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000070"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000070"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0005-000000000071"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000005"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000071"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },

                // ===== Cinema Reels Partners =====
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000001"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000001"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000002"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000002"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000003"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000003"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000004"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000004"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000005"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000005"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000006"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000006"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000007"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000007"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000008"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000008"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000009"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000009"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000010"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000010"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000011"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000011"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000012"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000012"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000013"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000013"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000014"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000014"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000015"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000015"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000016"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000016"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000017"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000017"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000018"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000018"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000019"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000019"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000020"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000020"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000021"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000021"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000022"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000022"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000023"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000023"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000024"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000024"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000025"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000025"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000026"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000026"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000027"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000027"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000028"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000028"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000029"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000029"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000030"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000030"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000031"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000031"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000032"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000032"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000033"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000033"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000034"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000034"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000035"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000035"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000036"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000036"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000037"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000037"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000038"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000038"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000039"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000039"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000040"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000040"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000041"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000041"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000042"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000042"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000043"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000043"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000044"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000044"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000045"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000045"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000046"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000046"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000047"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000047"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000048"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000048"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000049"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000049"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000050"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000050"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000051"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000051"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000052"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000052"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000053"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000053"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000054"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000054"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000055"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000055"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000056"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000056"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000057"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000057"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000058"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000058"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000059"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000059"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000060"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000060"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000061"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000061"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000062"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000062"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000063"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000063"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000064"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000064"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000065"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000065"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000066"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000066"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000067"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000067"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000068"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000068"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000069"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000069"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000070"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000070"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false },
                new Partner { Id = Guid.Parse("E0000000-0000-0000-0006-000000000071"), CompanyId = Guid.Parse("aabbccdd-aabb-aabb-aabb-000000000006"), CityId = Guid.Parse("D0000000-0000-0000-0000-000000000071"), Status = PartnerStatus.Available, CreatedAt = seedDate, IsDeleted = false }
            );

        }
    }
}
