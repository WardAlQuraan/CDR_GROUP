using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dropcompanysuccessreason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the table only if it actually exists.
            migrationBuilder.Sql("DROP TABLE IF EXISTS `CompanySuccessReasons`;");

            // Remove the 4 success-reason permissions and their RolePermission rows.
            // We use raw DELETE rather than EF's UpdateData because the
            // (RoleId, PermissionId) unique index on RolePermissions makes the
            // auto-generated index-shifting updates fail with duplicate-key errors.
            migrationBuilder.Sql(@"
                DELETE FROM `RolePermissions`
                WHERE `PermissionId` IN (
                    'bbccddee-4444-4444-4444-aaaaaaaaaaaa',
                    'bbccddee-4444-4444-4444-bbbbbbbbbbbb',
                    'bbccddee-4444-4444-4444-cccccccccccc',
                    'bbccddee-4444-4444-4444-dddddddddddd'
                );
                DELETE FROM `Permissions`
                WHERE `Id` IN (
                    'bbccddee-4444-4444-4444-aaaaaaaaaaaa',
                    'bbccddee-4444-4444-4444-bbbbbbbbbbbb',
                    'bbccddee-4444-4444-4444-cccccccccccc',
                    'bbccddee-4444-4444-4444-dddddddddddd'
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanySuccessReasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ReasonAr = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReasonEn = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySuccessReasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanySuccessReasons_Companies_CompanyId",
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
                    { new Guid("bbccddee-4444-4444-4444-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View company success reasons", false, "CompanySuccessReasons", "company-success-reasons.read", null, null },
                    { new Guid("bbccddee-4444-4444-4444-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create company success reasons", false, "CompanySuccessReasons", "company-success-reasons.create", null, null },
                    { new Guid("bbccddee-4444-4444-4444-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update company success reasons", false, "CompanySuccessReasons", "company-success-reasons.update", null, null },
                    { new Guid("bbccddee-4444-4444-4444-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete company success reasons", false, "CompanySuccessReasons", "company-success-reasons.delete", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanySuccessReasons_CompanyId",
                table: "CompanySuccessReasons",
                column: "CompanyId");
        }
    }
}
