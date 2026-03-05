using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NameEn = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NameAr = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    CountryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedAt", "CreatedBy", "IsDeleted", "Latitude", "Longitude", "NameAr", "NameEn", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.949999999999999, 35.93, "عمّان", "Amman", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.560000000000002, 35.850000000000001, "إربد", "Irbid", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.07, 36.090000000000003, "الزرقاء", "Zarqa", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 29.530000000000001, 35.009999999999998, "العقبة", "Aqaba", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.270000000000003, 35.899999999999999, "جرش", "Jarash", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000006"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.719999999999999, 35.789999999999999, "مادبا", "Madaba", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000007"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.039999999999999, 35.729999999999997, "السلط", "Salt", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000008"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.340000000000003, 36.210000000000001, "المفرق", "Mafraq", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000009"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.18, 35.700000000000003, "الكرك", "Karak", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000010"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 30.84, 35.600000000000001, "الطفيلة", "Tafilah", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000011"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 30.199999999999999, 35.729999999999997, "معان", "Ma'an", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000012"), new Guid("c0000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.329999999999998, 35.75, "عجلون", "Ajloun", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000013"), new Guid("c0000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 24.710000000000001, 46.68, "الرياض", "Riyadh", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000014"), new Guid("c0000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 21.489999999999998, 39.189999999999998, "جدة", "Jeddah", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000015"), new Guid("c0000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 21.390000000000001, 39.859999999999999, "مكة المكرمة", "Mecca", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000016"), new Guid("c0000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 24.469999999999999, 39.609999999999999, "المدينة المنورة", "Medina", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000017"), new Guid("c0000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 26.43, 50.100000000000001, "الدمام", "Dammam", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000018"), new Guid("c0000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 28.379999999999999, 36.57, "تبوك", "Tabuk", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000019"), new Guid("c0000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 18.219999999999999, 42.5, "أبها", "Abha", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000020"), new Guid("c0000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 25.199999999999999, 55.270000000000003, "دبي", "Dubai", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000021"), new Guid("c0000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 24.449999999999999, 54.369999999999997, "أبو ظبي", "Abu Dhabi", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000022"), new Guid("c0000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 25.350000000000001, 55.390000000000001, "الشارقة", "Sharjah", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000023"), new Guid("c0000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 25.289999999999999, 51.530000000000001, "الدوحة", "Doha", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000024"), new Guid("c0000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 25.170000000000002, 51.600000000000001, "الوكرة", "Al Wakrah", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000025"), new Guid("c0000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 26.23, 50.590000000000003, "المنامة", "Manama", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000026"), new Guid("c0000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 26.260000000000002, 50.619999999999997, "المحرق", "Muharraq", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000027"), new Guid("c0000000-0000-0000-0000-000000000006"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 29.379999999999999, 47.990000000000002, "مدينة الكويت", "Kuwait City", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000028"), new Guid("c0000000-0000-0000-0000-000000000006"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 29.329999999999998, 48.030000000000001, "حولي", "Hawalli", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000029"), new Guid("c0000000-0000-0000-0000-000000000007"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 23.59, 58.549999999999997, "مسقط", "Muscat", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000030"), new Guid("c0000000-0000-0000-0000-000000000007"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 17.02, 54.090000000000003, "صلالة", "Salalah", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000031"), new Guid("c0000000-0000-0000-0000-000000000008"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.369999999999999, 44.210000000000001, "صنعاء", "Sana'a", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000032"), new Guid("c0000000-0000-0000-0000-000000000008"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 12.789999999999999, 45.020000000000003, "عدن", "Aden", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000033"), new Guid("c0000000-0000-0000-0000-000000000009"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 33.310000000000002, 44.369999999999997, "بغداد", "Baghdad", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000034"), new Guid("c0000000-0000-0000-0000-000000000009"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 30.510000000000002, 47.810000000000002, "البصرة", "Basra", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000035"), new Guid("c0000000-0000-0000-0000-000000000009"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 36.189999999999998, 44.009999999999998, "أربيل", "Erbil", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000036"), new Guid("c0000000-0000-0000-0000-000000000010"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 33.509999999999998, 36.289999999999999, "دمشق", "Damascus", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000037"), new Guid("c0000000-0000-0000-0000-000000000010"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 36.200000000000003, 37.159999999999997, "حلب", "Aleppo", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000038"), new Guid("c0000000-0000-0000-0000-000000000011"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 33.890000000000001, 35.5, "بيروت", "Beirut", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000039"), new Guid("c0000000-0000-0000-0000-000000000011"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 34.439999999999998, 35.829999999999998, "طرابلس", "Tripoli", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000040"), new Guid("c0000000-0000-0000-0000-000000000012"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.77, 35.229999999999997, "القدس", "Jerusalem", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000041"), new Guid("c0000000-0000-0000-0000-000000000012"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.5, 34.469999999999999, "غزة", "Gaza", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000042"), new Guid("c0000000-0000-0000-0000-000000000012"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.899999999999999, 35.200000000000003, "رام الله", "Ramallah", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000043"), new Guid("c0000000-0000-0000-0000-000000000013"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 30.039999999999999, 31.239999999999998, "القاهرة", "Cairo", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000044"), new Guid("c0000000-0000-0000-0000-000000000013"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 31.199999999999999, 29.920000000000002, "الإسكندرية", "Alexandria", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000045"), new Guid("c0000000-0000-0000-0000-000000000014"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 39.93, 32.850000000000001, "أنقرة", "Ankara", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000046"), new Guid("c0000000-0000-0000-0000-000000000014"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 41.009999999999998, 28.98, "إسطنبول", "Istanbul", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000047"), new Guid("c0000000-0000-0000-0000-000000000015"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 35.689999999999998, 51.390000000000001, "طهران", "Tehran", null, null },
                    { new Guid("d0000000-0000-0000-0000-000000000048"), new Guid("c0000000-0000-0000-0000-000000000015"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 32.649999999999999, 51.68, "أصفهان", "Isfahan", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
