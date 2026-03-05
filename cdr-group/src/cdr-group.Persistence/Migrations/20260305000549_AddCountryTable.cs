using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NameEn = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameAr = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Latitude", "Longitude", "NameAr", "NameEn", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.949999999999999, 35.93, "الأردن", "Jordan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 23.879999999999999, 45.079999999999998, "المملكة العربية السعودية", "Saudi Arabia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 23.420000000000002, 53.850000000000001, "الإمارات العربية المتحدة", "UAE", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 25.350000000000001, 51.18, "قطر", "Qatar", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 26.07, 50.549999999999997, "البحرين", "Bahrain", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000006"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 29.309999999999999, 47.479999999999997, "الكويت", "Kuwait", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000007"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 21.469999999999999, 55.979999999999997, "عُمان", "Oman", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000008"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.550000000000001, 48.520000000000003, "اليمن", "Yemen", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000009"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 33.219999999999999, 43.68, "العراق", "Iraq", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000010"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 34.799999999999997, 38.990000000000002, "سوريا", "Syria", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000011"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 33.850000000000001, 35.859999999999999, "لبنان", "Lebanon", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000012"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.949999999999999, 35.229999999999997, "فلسطين", "Palestine", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000013"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 26.82, 30.800000000000001, "مصر", "Egypt", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000014"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 38.960000000000001, 35.240000000000002, "تركيا", "Turkey", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000015"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.43, 53.689999999999998, "إيران", "Iran", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000016"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 26.34, 17.23, "ليبيا", "Libya", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000017"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 12.859999999999999, 30.219999999999999, "السودان", "Sudan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 33.890000000000001, 9.5399999999999991, "تونس", "Tunisia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000019"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 28.030000000000001, 1.6599999999999999, "الجزائر", "Algeria", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000020"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.789999999999999, -7.0899999999999999, "المغرب", "Morocco", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000021"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 21.010000000000002, -10.94, "موريتانيا", "Mauritania", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000022"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 5.1500000000000004, 46.200000000000003, "الصومال", "Somalia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000023"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 11.83, 42.590000000000003, "جيبوتي", "Djibouti", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000024"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -11.880000000000001, 43.869999999999997, "جزر القمر", "Comoros", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
