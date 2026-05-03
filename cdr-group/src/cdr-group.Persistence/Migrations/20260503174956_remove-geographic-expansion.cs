using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removegeographicexpansion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the table only if it actually exists in this database.
            migrationBuilder.Sql("DROP TABLE IF EXISTS `CompanyGeographicExpansions`;");

            // Remove the 4 geographic-expansion permissions.
            // We delete dependent RolePermissions explicitly first to be safe,
            // since the `(RoleId, PermissionId)` unique index makes
            // EF's auto-generated UpdateData renumbering unsafe — those updates
            // collide with rows that already hold the target PermissionId.
            migrationBuilder.Sql(@"
                DELETE FROM `RolePermissions`
                WHERE `PermissionId` IN (
                    'bbccddee-8888-8888-8888-aaaaaaaaaaaa',
                    'bbccddee-8888-8888-8888-bbbbbbbbbbbb',
                    'bbccddee-8888-8888-8888-cccccccccccc',
                    'bbccddee-8888-8888-8888-dddddddddddd'
                );
                DELETE FROM `Permissions`
                WHERE `Id` IN (
                    'bbccddee-8888-8888-8888-aaaaaaaaaaaa',
                    'bbccddee-8888-8888-8888-bbbbbbbbbbbb',
                    'bbccddee-8888-8888-8888-cccccccccccc',
                    'bbccddee-8888-8888-8888-dddddddddddd'
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyGeographicExpansions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionAr = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionEn = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TitleAr = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleEn = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyGeographicExpansions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyGeographicExpansions_Companies_CompanyId",
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
                    { new Guid("bbccddee-8888-8888-8888-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View company geographic expansions", false, "CompanyGeographicExpansions", "company-geographic-expansions.read", null, null },
                    { new Guid("bbccddee-8888-8888-8888-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create company geographic expansions", false, "CompanyGeographicExpansions", "company-geographic-expansions.create", null, null },
                    { new Guid("bbccddee-8888-8888-8888-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update company geographic expansions", false, "CompanyGeographicExpansions", "company-geographic-expansions.update", null, null },
                    { new Guid("bbccddee-8888-8888-8888-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete company geographic expansions", false, "CompanyGeographicExpansions", "company-geographic-expansions.delete", null, null }
                });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"),
                column: "PermissionId",
                value: new Guid("bbccddee-8888-8888-8888-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"),
                column: "PermissionId",
                value: new Guid("bbccddee-8888-8888-8888-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"),
                column: "PermissionId",
                value: new Guid("bbccddee-8888-8888-8888-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"),
                column: "PermissionId",
                value: new Guid("bbccddee-8888-8888-8888-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000105"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000091"),
                column: "PermissionId",
                value: new Guid("bbccddee-8888-8888-8888-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000092"),
                column: "PermissionId",
                value: new Guid("bbccddee-8888-8888-8888-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000093"),
                column: "PermissionId",
                value: new Guid("bbccddee-8888-8888-8888-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000094"),
                column: "PermissionId",
                value: new Guid("bbccddee-8888-8888-8888-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000095"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000096"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000097"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000098"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000099"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000100"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000101"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000102"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000103"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000104"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000105"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000106"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-dddddddddddd"));

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
                name: "IX_CompanyGeographicExpansions_CompanyId",
                table: "CompanyGeographicExpansions",
                column: "CompanyId");
        }
    }
}
