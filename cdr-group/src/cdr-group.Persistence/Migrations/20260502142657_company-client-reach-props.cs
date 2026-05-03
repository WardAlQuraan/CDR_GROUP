using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class companyclientreachprops : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "CompanyClientReaches",
                newName: "DescriptionEn");

            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "CompanyClientReaches",
                newName: "ClientNameEn");

            migrationBuilder.AddColumn<string>(
                name: "ClientNameAr",
                table: "CompanyClientReaches",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAr",
                table: "CompanyClientReaches",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientNameAr",
                table: "CompanyClientReaches");

            migrationBuilder.DropColumn(
                name: "DescriptionAr",
                table: "CompanyClientReaches");

            migrationBuilder.RenameColumn(
                name: "DescriptionEn",
                table: "CompanyClientReaches",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ClientNameEn",
                table: "CompanyClientReaches",
                newName: "ClientName");
        }
    }
}
