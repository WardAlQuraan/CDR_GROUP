using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class companytitledescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyTitleDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Code = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleEn = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleAr = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionEn = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionAr = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
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
                    table.PrimaryKey("PK_CompanyTitleDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTitleDescriptions_Companies_CompanyId",
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
                    { new Guid("ccddeeff-3333-3333-3333-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View company title descriptions", false, "CompanyTitleDescriptions", "company-title-descriptions.read", null, null },
                    { new Guid("ccddeeff-3333-3333-3333-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create company title descriptions", false, "CompanyTitleDescriptions", "company-title-descriptions.create", null, null },
                    { new Guid("ccddeeff-3333-3333-3333-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update company title descriptions", false, "CompanyTitleDescriptions", "company-title-descriptions.update", null, null },
                    { new Guid("ccddeeff-3333-3333-3333-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete company title descriptions", false, "CompanyTitleDescriptions", "company-title-descriptions.delete", null, null }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "PermissionId", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000110"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000112"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000113"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-dddddddddddd"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000107"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-aaaaaaaaaaaa"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000108"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-bbbbbbbbbbbb"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000109"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-cccccccccccc"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000110"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-dddddddddddd"), new Guid("55555555-5555-5555-5555-555555555555"), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTitleDescriptions_CompanyId_Code",
                table: "CompanyTitleDescriptions",
                columns: new[] { "CompanyId", "Code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyTitleDescriptions");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000110"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000111"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000112"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000113"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000107"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000108"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000109"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000110"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ccddeeff-3333-3333-3333-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ccddeeff-3333-3333-3333-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ccddeeff-3333-3333-3333-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ccddeeff-3333-3333-3333-dddddddddddd"));
        }
    }
}
