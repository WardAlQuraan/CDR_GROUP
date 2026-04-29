using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class companypreferencevalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "CompanyPreferences",
                newName: "ValueEn");

            migrationBuilder.AddColumn<string>(
                name: "ValueAr",
                table: "CompanyPreferences",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueAr",
                table: "CompanyPreferences");

            migrationBuilder.RenameColumn(
                name: "ValueEn",
                table: "CompanyPreferences",
                newName: "Value");
        }
    }
}
