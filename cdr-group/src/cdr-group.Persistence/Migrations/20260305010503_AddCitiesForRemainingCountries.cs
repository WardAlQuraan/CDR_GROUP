using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCitiesForRemainingCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedAt", "CreatedBy", "IsDeleted", "Latitude", "Longitude", "NameAr", "NameEn", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("d0000000-0000-0000-0000-000000000049"), new Guid("c0000000-0000-0000-0000-000000000016"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.899999999999999, 13.18, "طرابلس", "Tripoli", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000050"), new Guid("c0000000-0000-0000-0000-000000000016"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.119999999999997, 20.07, "بنغازي", "Benghazi", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000051"), new Guid("c0000000-0000-0000-0000-000000000016"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.380000000000003, 15.09, "مصراتة", "Misrata", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000052"), new Guid("c0000000-0000-0000-0000-000000000017"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.5, 32.560000000000002, "الخرطوم", "Khartoum", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000053"), new Guid("c0000000-0000-0000-0000-000000000017"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.640000000000001, 32.479999999999997, "أم درمان", "Omdurman", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000054"), new Guid("c0000000-0000-0000-0000-000000000017"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 19.620000000000001, 37.219999999999999, "بورتسودان", "Port Sudan", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000055"), new Guid("c0000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 36.810000000000002, 10.17, "تونس", "Tunis", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000056"), new Guid("c0000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 34.740000000000002, 10.76, "صفاقس", "Sfax", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000057"), new Guid("c0000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 35.829999999999998, 10.59, "سوسة", "Sousse", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000058"), new Guid("c0000000-0000-0000-0000-000000000019"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 36.75, 3.04, "الجزائر", "Algiers", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000059"), new Guid("c0000000-0000-0000-0000-000000000019"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 35.700000000000003, -0.63, "وهران", "Oran", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000060"), new Guid("c0000000-0000-0000-0000-000000000019"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 36.369999999999997, 6.6100000000000003, "قسنطينة", "Constantine", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000061"), new Guid("c0000000-0000-0000-0000-000000000020"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 34.020000000000003, -6.8399999999999999, "الرباط", "Rabat", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000062"), new Guid("c0000000-0000-0000-0000-000000000020"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 33.57, -7.5899999999999999, "الدار البيضاء", "Casablanca", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000063"), new Guid("c0000000-0000-0000-0000-000000000020"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.629999999999999, -8.0099999999999998, "مراكش", "Marrakech", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000064"), new Guid("c0000000-0000-0000-0000-000000000021"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 18.09, -15.98, "نواكشوط", "Nouakchott", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000065"), new Guid("c0000000-0000-0000-0000-000000000021"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 20.940000000000001, -17.039999999999999, "نواذيبو", "Nouadhibou", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000066"), new Guid("c0000000-0000-0000-0000-000000000022"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 2.0499999999999998, 45.32, "مقديشو", "Mogadishu", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000067"), new Guid("c0000000-0000-0000-0000-000000000022"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 9.5600000000000005, 44.060000000000002, "هرجيسا", "Hargeisa", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000068"), new Guid("c0000000-0000-0000-0000-000000000023"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 11.59, 43.149999999999999, "مدينة جيبوتي", "Djibouti City", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000069"), new Guid("c0000000-0000-0000-0000-000000000023"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 11.16, 42.710000000000001, "علي صبيح", "Ali Sabieh", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000070"), new Guid("c0000000-0000-0000-0000-000000000024"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -11.699999999999999, 43.259999999999998, "موروني", "Moroni", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000071"), new Guid("c0000000-0000-0000-0000-000000000024"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -12.17, 44.399999999999999, "موتسامودو", "Mutsamudu", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000062"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000063"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000064"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000065"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000066"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000067"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000068"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000069"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000070"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000071"));
        }
    }
}
