using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dropcompanyfinancialclausesrights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the table only if it actually exists.
            migrationBuilder.Sql("DROP TABLE IF EXISTS `CompanyFinancialClausesRights`;");

            // Remove the 4 financial-clauses-rights permissions and their RolePermission rows.
            // We use raw DELETE rather than EF's UpdateData because the
            // (RoleId, PermissionId) unique index on RolePermissions makes the
            // auto-generated index-shifting updates fail with duplicate-key errors.
            migrationBuilder.Sql(@"
                DELETE FROM `RolePermissions`
                WHERE `PermissionId` IN (
                    'ccddeeff-1111-1111-1111-aaaaaaaaaaaa',
                    'ccddeeff-1111-1111-1111-bbbbbbbbbbbb',
                    'ccddeeff-1111-1111-1111-cccccccccccc',
                    'ccddeeff-1111-1111-1111-dddddddddddd'
                );
                DELETE FROM `Permissions`
                WHERE `Id` IN (
                    'ccddeeff-1111-1111-1111-aaaaaaaaaaaa',
                    'ccddeeff-1111-1111-1111-bbbbbbbbbbbb',
                    'ccddeeff-1111-1111-1111-cccccccccccc',
                    'ccddeeff-1111-1111-1111-dddddddddddd'
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyFinancialClausesRights",
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
                    table.PrimaryKey("PK_CompanyFinancialClausesRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyFinancialClausesRights_Companies_CompanyId",
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
                    { new Guid("ccddeeff-1111-1111-1111-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View company financial clauses rights", false, "CompanyFinancialClausesRights", "company-financial-clauses-rights.read", null, null },
                    { new Guid("ccddeeff-1111-1111-1111-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create company financial clauses rights", false, "CompanyFinancialClausesRights", "company-financial-clauses-rights.create", null, null },
                    { new Guid("ccddeeff-1111-1111-1111-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update company financial clauses rights", false, "CompanyFinancialClausesRights", "company-financial-clauses-rights.update", null, null },
                    { new Guid("ccddeeff-1111-1111-1111-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete company financial clauses rights", false, "CompanyFinancialClausesRights", "company-financial-clauses-rights.delete", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFinancialClausesRights_CompanyId",
                table: "CompanyFinancialClausesRights",
                column: "CompanyId");
        }
    }
}
