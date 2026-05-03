using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class companybranchaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "CompanyBranches",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "CompanyBranches",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "CompanyBranches",
                type: "double",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "CompanyBranches");
        }
    }
}
