using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dropcompanypartnershipfranchisemechanism : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPartnershipFranchiseMechanisms");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbccddee-9999-9999-9999-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbccddee-9999-9999-9999-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbccddee-9999-9999-9999-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("bbccddee-9999-9999-9999-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000087"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000088"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000089"));

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000090"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                column: "PermissionId",
                value: new Guid("ccddeeff-3333-3333-3333-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                column: "PermissionId",
                value: new Guid("ccddeeff-3333-3333-3333-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                column: "PermissionId",
                value: new Guid("ccddeeff-3333-3333-3333-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                column: "PermissionId",
                value: new Guid("ccddeeff-3333-3333-3333-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000075"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000076"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000077"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000078"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000079"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000080"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000081"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000082"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000083"),
                column: "PermissionId",
                value: new Guid("ccddeeff-3333-3333-3333-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000084"),
                column: "PermissionId",
                value: new Guid("ccddeeff-3333-3333-3333-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000085"),
                column: "PermissionId",
                value: new Guid("ccddeeff-3333-3333-3333-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000086"),
                column: "PermissionId",
                value: new Guid("ccddeeff-3333-3333-3333-dddddddddddd"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyPartnershipFranchiseMechanisms",
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
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPartnershipFranchiseMechanisms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPartnershipFranchiseMechanisms_Companies_CompanyId",
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
                    { new Guid("bbccddee-9999-9999-9999-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View company partnership franchise mechanisms", false, "CompanyPartnershipFranchiseMechanisms", "company-partnership-franchise-mechanisms.read", null, null },
                    { new Guid("bbccddee-9999-9999-9999-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create company partnership franchise mechanisms", false, "CompanyPartnershipFranchiseMechanisms", "company-partnership-franchise-mechanisms.create", null, null },
                    { new Guid("bbccddee-9999-9999-9999-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update company partnership franchise mechanisms", false, "CompanyPartnershipFranchiseMechanisms", "company-partnership-franchise-mechanisms.update", null, null },
                    { new Guid("bbccddee-9999-9999-9999-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete company partnership franchise mechanisms", false, "CompanyPartnershipFranchiseMechanisms", "company-partnership-franchise-mechanisms.delete", null, null }
                });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000075"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000076"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000077"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000078"),
                column: "PermissionId",
                value: new Guid("bbccddee-9999-9999-9999-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000079"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000080"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000081"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000082"),
                column: "PermissionId",
                value: new Guid("ccddeeff-1111-1111-1111-dddddddddddd"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000083"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000084"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000085"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-cccccccccccc"));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-1111-000000000086"),
                column: "PermissionId",
                value: new Guid("ccddeeff-2222-2222-2222-dddddddddddd"));

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "PermissionId", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000090"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-aaaaaaaaaaaa"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000091"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-bbbbbbbbbbbb"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000092"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-cccccccccccc"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-0000-000000000093"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-dddddddddddd"), new Guid("11111111-1111-1111-1111-111111111111"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000087"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-aaaaaaaaaaaa"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000088"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-bbbbbbbbbbbb"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000089"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-cccccccccccc"), new Guid("55555555-5555-5555-5555-555555555555"), null, null },
                    { new Guid("00000000-0000-0000-1111-000000000090"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("ccddeeff-3333-3333-3333-dddddddddddd"), new Guid("55555555-5555-5555-5555-555555555555"), null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPartnershipFranchiseMechanisms_CompanyId",
                table: "CompanyPartnershipFranchiseMechanisms",
                column: "CompanyId");
        }
    }
}
