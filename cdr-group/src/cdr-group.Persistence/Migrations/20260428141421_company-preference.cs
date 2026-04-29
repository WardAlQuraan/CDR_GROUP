using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class companypreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyPreferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPreferences_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Module", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("bbccddee-2222-2222-2222-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View company preferences", false, "CompanyPreferences", "company-preferences.read", null, null },
                    { new Guid("bbccddee-2222-2222-2222-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create company preferences", false, "CompanyPreferences", "company-preferences.create", null, null },
                    { new Guid("bbccddee-2222-2222-2222-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update company preferences", false, "CompanyPreferences", "company-preferences.update", null, null },
                    { new Guid("bbccddee-2222-2222-2222-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete company preferences", false, "CompanyPreferences", "company-preferences.delete", null, null }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "PermissionId", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000070"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("bbccddee-2222-2222-2222-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000071"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("bbccddee-2222-2222-2222-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000072"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("bbccddee-2222-2222-2222-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000073"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("bbccddee-2222-2222-2222-dddddddddddd"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000067"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("bbccddee-2222-2222-2222-aaaaaaaaaaaa"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000068"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("bbccddee-2222-2222-2222-bbbbbbbbbbbb"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000069"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("bbccddee-2222-2222-2222-cccccccccccc"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000070"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("bbccddee-2222-2222-2222-dddddddddddd"), new Guid("55555555-5555-5555-5555-555555555555"), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPreferences_CompanyId_Code",
                table: "CompanyPreferences",
                columns: new[] { "CompanyId", "Code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPreferences");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000067"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000068"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000069"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000070"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbccddee-2222-2222-2222-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbccddee-2222-2222-2222-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbccddee-2222-2222-2222-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbccddee-2222-2222-2222-dddddddddddd"));
        }
    }
}
