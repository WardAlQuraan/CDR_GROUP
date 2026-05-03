using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class companyclientreach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyClientReaches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClientName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientLogoUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reach = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
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
                    table.PrimaryKey("PK_CompanyClientReaches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyClientReaches_Companies_CompanyId",
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
                    { new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View company client reaches", false, "CompanyClientReaches", "company-client-reaches.read", null, null },
                    { new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create company client reaches", false, "CompanyClientReaches", "company-client-reaches.create", null, null },
                    { new Guid("ccddeeff-2222-2222-2222-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update company client reaches", false, "CompanyClientReaches", "company-client-reaches.update", null, null },
                    { new Guid("ccddeeff-2222-2222-2222-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete company client reaches", false, "CompanyClientReaches", "company-client-reaches.delete", null, null }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "PermissionId", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000106"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000107"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000108"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-2222-2222-2222-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000109"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-2222-2222-2222-dddddddddddd"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000103"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000104"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000105"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-2222-2222-2222-cccccccccccc"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000106"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-2222-2222-2222-dddddddddddd"), new Guid("55555555-5555-5555-5555-555555555555"), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyClientReaches_CompanyId",
                table: "CompanyClientReaches",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyClientReaches");

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000106"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000107"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000108"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000109"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000103"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000104"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000105"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000106"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ccddeeff-2222-2222-2222-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ccddeeff-2222-2222-2222-dddddddddddd"));
        }
    }
}
