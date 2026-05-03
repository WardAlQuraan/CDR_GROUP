using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class companytitledescriptionremoveconstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // MySQL won't drop the unique compound index while it's the only one covering
            // the FK on CompanyId. Add a temporary index, swap, then drop the temp.
            migrationBuilder.CreateIndex(
                name: "IX_CompanyTitleDescriptions_CompanyId_FK_Temp",
                table: "CompanyTitleDescriptions",
                column: "CompanyId");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTitleDescriptions_CompanyId_Code",
                table: "CompanyTitleDescriptions");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTitleDescriptions_CompanyId_Code",
                table: "CompanyTitleDescriptions",
                columns: new[] { "CompanyId", "Code" });

            migrationBuilder.DropIndex(
                name: "IX_CompanyTitleDescriptions_CompanyId_FK_Temp",
                table: "CompanyTitleDescriptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CompanyTitleDescriptions_CompanyId_FK_Temp",
                table: "CompanyTitleDescriptions",
                column: "CompanyId");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTitleDescriptions_CompanyId_Code",
                table: "CompanyTitleDescriptions");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTitleDescriptions_CompanyId_Code",
                table: "CompanyTitleDescriptions",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.DropIndex(
                name: "IX_CompanyTitleDescriptions_CompanyId_FK_Temp",
                table: "CompanyTitleDescriptions");
        }
    }
}
