using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class companybranchimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CompanyBranches",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CompanyBranches");
        }
    }
}
