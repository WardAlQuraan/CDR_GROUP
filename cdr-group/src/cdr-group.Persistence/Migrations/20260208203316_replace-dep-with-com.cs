using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class replacedepwithcom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Departments_DepartmentId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_DepartmentId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Events",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Events_DepartmentId",
                table: "Events",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Departments_DepartmentId",
                table: "Events",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
