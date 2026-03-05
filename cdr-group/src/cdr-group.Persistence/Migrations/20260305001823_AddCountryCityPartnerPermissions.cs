using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryCityPartnerPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Module", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("aabbccdd-6666-6666-6666-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View countries", false, "Countries", "countries.read", null, null },
                    { new Guid("aabbccdd-6666-6666-6666-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create countries", false, "Countries", "countries.create", null, null },
                    { new Guid("aabbccdd-6666-6666-6666-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update countries", false, "Countries", "countries.update", null, null },
                    { new Guid("aabbccdd-6666-6666-6666-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete countries", false, "Countries", "countries.delete", null, null },
                    { new Guid("aabbccdd-7777-7777-7777-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View cities", false, "Cities", "cities.read", null, null },
                    { new Guid("aabbccdd-7777-7777-7777-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create cities", false, "Cities", "cities.create", null, null },
                    { new Guid("aabbccdd-7777-7777-7777-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update cities", false, "Cities", "cities.update", null, null },
                    { new Guid("aabbccdd-7777-7777-7777-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete cities", false, "Cities", "cities.delete", null, null },
                    { new Guid("aabbccdd-8888-8888-8888-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View partners", false, "Partners", "partners.read", null, null },
                    { new Guid("aabbccdd-8888-8888-8888-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create partners", false, "Partners", "partners.create", null, null },
                    { new Guid("aabbccdd-8888-8888-8888-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update partners", false, "Partners", "partners.update", null, null },
                    { new Guid("aabbccdd-8888-8888-8888-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete partners", false, "Partners", "partners.delete", null, null }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "PermissionId", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000050"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-6666-6666-6666-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000051"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-6666-6666-6666-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000052"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-6666-6666-6666-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000053"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-6666-6666-6666-dddddddddddd"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000054"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-7777-7777-7777-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000055"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-7777-7777-7777-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000056"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-7777-7777-7777-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000057"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-7777-7777-7777-dddddddddddd"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000058"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-8888-8888-8888-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000059"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-8888-8888-8888-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000060"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-8888-8888-8888-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000061"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-8888-8888-8888-dddddddddddd"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000047"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-6666-6666-6666-aaaaaaaaaaaa"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000048"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-6666-6666-6666-bbbbbbbbbbbb"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000049"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-6666-6666-6666-cccccccccccc"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000050"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-6666-6666-6666-dddddddddddd"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000051"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-7777-7777-7777-aaaaaaaaaaaa"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000052"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-7777-7777-7777-bbbbbbbbbbbb"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000053"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-7777-7777-7777-cccccccccccc"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000054"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-7777-7777-7777-dddddddddddd"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000055"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-8888-8888-8888-aaaaaaaaaaaa"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000056"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-8888-8888-8888-bbbbbbbbbbbb"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000057"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-8888-8888-8888-cccccccccccc"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000058"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("aabbccdd-8888-8888-8888-dddddddddddd"), new Guid("55555555-5555-5555-5555-555555555555"), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000047"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000048"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000049"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000050"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000051"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000052"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000053"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000054"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000055"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000056"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000057"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000058"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-6666-6666-6666-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-6666-6666-6666-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-6666-6666-6666-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-6666-6666-6666-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-7777-7777-7777-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-7777-7777-7777-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-7777-7777-7777-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-7777-7777-7777-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-8888-8888-8888-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-8888-8888-8888-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-8888-8888-8888-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-8888-8888-8888-dddddddddddd"));
        }
    }
}
