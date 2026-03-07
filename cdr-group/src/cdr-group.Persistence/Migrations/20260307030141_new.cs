using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId1",
                table: "Partners",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_CompanyId1",
                table: "Partners",
                column: "CompanyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Companies_CompanyId1",
                table: "Partners",
                column: "CompanyId1",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Companies_CompanyId1",
                table: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_Partners_CompanyId1",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "Partners");
        }
    }
}
