using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOpeningHoursToCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OpeningEndDay",
                table: "Companies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningEndTime",
                table: "Companies",
                type: "time(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpeningHoursNoteAr",
                table: "Companies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OpeningHoursNoteEn",
                table: "Companies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "OpeningStartDay",
                table: "Companies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OpeningStartTime",
                table: "Companies",
                type: "time(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-aabb-aabb-aabb-aabbccddeeff"),
                columns: new[] { "OpeningEndDay", "OpeningEndTime", "OpeningHoursNoteAr", "OpeningHoursNoteEn", "OpeningStartDay", "OpeningStartTime" },
                values: new object[] { "Thursday", new TimeSpan(0, 17, 0, 0, 0), null, null, "Sunday", new TimeSpan(0, 9, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningEndDay",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "OpeningEndTime",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "OpeningHoursNoteAr",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "OpeningHoursNoteEn",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "OpeningStartDay",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "OpeningStartTime",
                table: "Companies");
        }
    }
}
