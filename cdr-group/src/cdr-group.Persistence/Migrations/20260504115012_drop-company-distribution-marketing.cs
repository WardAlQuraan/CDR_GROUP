using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dropcompanydistributionmarketing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the table only if it actually exists.
            migrationBuilder.Sql("DROP TABLE IF EXISTS `CompanyDistributionMarketings`;");

            // Remove the 4 distribution-marketing permissions and their RolePermission rows.
            // Raw DELETE avoids the (RoleId, PermissionId) duplicate-key collisions
            // that EF's auto-generated UpdateData renumbering produces.
            migrationBuilder.Sql(@"
                DELETE FROM `RolePermissions`
                WHERE `PermissionId` IN (
                    'bbccddee-6666-6666-6666-aaaaaaaaaaaa',
                    'bbccddee-6666-6666-6666-bbbbbbbbbbbb',
                    'bbccddee-6666-6666-6666-cccccccccccc',
                    'bbccddee-6666-6666-6666-dddddddddddd'
                );
                DELETE FROM `Permissions`
                WHERE `Id` IN (
                    'bbccddee-6666-6666-6666-aaaaaaaaaaaa',
                    'bbccddee-6666-6666-6666-bbbbbbbbbbbb',
                    'bbccddee-6666-6666-6666-cccccccccccc',
                    'bbccddee-6666-6666-6666-dddddddddddd'
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyDistributionMarketings",
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
                    table.PrimaryKey("PK_CompanyDistributionMarketings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDistributionMarketings_Companies_CompanyId",
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
                    { new Guid("bbccddee-6666-6666-6666-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View company distribution marketings", false, "CompanyDistributionMarketings", "company-distribution-marketings.read", null, null },
                    { new Guid("bbccddee-6666-6666-6666-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create company distribution marketings", false, "CompanyDistributionMarketings", "company-distribution-marketings.create", null, null },
                    { new Guid("bbccddee-6666-6666-6666-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update company distribution marketings", false, "CompanyDistributionMarketings", "company-distribution-marketings.update", null, null },
                    { new Guid("bbccddee-6666-6666-6666-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete company distribution marketings", false, "CompanyDistributionMarketings", "company-distribution-marketings.delete", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDistributionMarketings_CompanyId",
                table: "CompanyDistributionMarketings",
                column: "CompanyId");
        }
    }
}
