using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dropcompanydistinguish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS `CompanyDistinguishes`;");

            migrationBuilder.Sql(@"
                DELETE FROM `RolePermissions`
                WHERE `PermissionId` IN (
                    'bbccddee-5555-5555-5555-aaaaaaaaaaaa',
                    'bbccddee-5555-5555-5555-bbbbbbbbbbbb',
                    'bbccddee-5555-5555-5555-cccccccccccc',
                    'bbccddee-5555-5555-5555-dddddddddddd'
                );
                DELETE FROM `Permissions`
                WHERE `Id` IN (
                    'bbccddee-5555-5555-5555-aaaaaaaaaaaa',
                    'bbccddee-5555-5555-5555-bbbbbbbbbbbb',
                    'bbccddee-5555-5555-5555-cccccccccccc',
                    'bbccddee-5555-5555-5555-dddddddddddd'
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyDistinguishes",
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
                    TitleAr = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleEn = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDistinguishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDistinguishes_Companies_CompanyId",
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
                    { new Guid("bbccddee-5555-5555-5555-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "View company distinguishes", false, "CompanyDistinguishes", "company-distinguishes.read", null, null },
                    { new Guid("bbccddee-5555-5555-5555-bbbbbbbbbbbb"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Create company distinguishes", false, "CompanyDistinguishes", "company-distinguishes.create", null, null },
                    { new Guid("bbccddee-5555-5555-5555-cccccccccccc"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Update company distinguishes", false, "CompanyDistinguishes", "company-distinguishes.update", null, null },
                    { new Guid("bbccddee-5555-5555-5555-dddddddddddd"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delete company distinguishes", false, "CompanyDistinguishes", "company-distinguishes.delete", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDistinguishes_CompanyId",
                table: "CompanyDistinguishes",
                column: "CompanyId");
        }
    }
}
