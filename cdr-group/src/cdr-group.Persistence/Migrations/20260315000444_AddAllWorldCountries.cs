using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAllWorldCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Latitude", "Longitude", "NameAr", "NameEn", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("c0000000-0000-0000-0000-000000000025"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 41.149999999999999, 20.170000000000002, "ألبانيا", "Albania", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000026"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 42.549999999999997, 1.52, "أندورا", "Andorra", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000027"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 47.520000000000003, 14.550000000000001, "النمسا", "Austria", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000028"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 53.710000000000001, 27.949999999999999, "بيلاروسيا", "Belarus", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000029"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 50.850000000000001, 4.3499999999999996, "بلجيكا", "Belgium", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000030"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 43.920000000000002, 17.68, "البوسنة والهرسك", "Bosnia and Herzegovina", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000031"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 42.729999999999997, 25.489999999999998, "بلغاريا", "Bulgaria", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000032"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 45.100000000000001, 15.199999999999999, "كرواتيا", "Croatia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000033"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 35.130000000000003, 33.43, "قبرص", "Cyprus", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000034"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 49.82, 15.470000000000001, "التشيك", "Czech Republic", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000035"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 56.259999999999998, 9.5, "الدنمارك", "Denmark", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000036"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 58.600000000000001, 25.010000000000002, "إستونيا", "Estonia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000037"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 61.920000000000002, 25.75, "فنلندا", "Finland", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000038"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 46.229999999999997, 2.21, "فرنسا", "France", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000039"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 51.170000000000002, 10.449999999999999, "ألمانيا", "Germany", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000040"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 39.07, 21.82, "اليونان", "Greece", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000041"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 47.159999999999997, 19.5, "المجر", "Hungary", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000042"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 64.959999999999994, -19.02, "آيسلندا", "Iceland", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000043"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 53.140000000000001, -7.6900000000000004, "أيرلندا", "Ireland", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000044"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 41.869999999999997, 12.57, "إيطاليا", "Italy", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000045"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 42.600000000000001, 20.899999999999999, "كوسوفو", "Kosovo", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000046"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 56.880000000000003, 24.600000000000001, "لاتفيا", "Latvia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000047"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 47.170000000000002, 9.5600000000000005, "ليختنشتاين", "Liechtenstein", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000048"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 55.170000000000002, 23.879999999999999, "ليتوانيا", "Lithuania", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000049"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 49.82, 6.1299999999999999, "لوكسمبورغ", "Luxembourg", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000050"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 35.939999999999998, 14.380000000000001, "مالطا", "Malta", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000051"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 47.409999999999997, 28.370000000000001, "مولدوفا", "Moldova", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000052"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 43.75, 7.4199999999999999, "موناكو", "Monaco", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000053"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 42.710000000000001, 19.370000000000001, "الجبل الأسود", "Montenegro", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000054"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 52.130000000000003, 5.29, "هولندا", "Netherlands", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000055"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 41.509999999999998, 21.75, "مقدونيا الشمالية", "North Macedonia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000056"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 60.469999999999999, 8.4700000000000006, "النرويج", "Norway", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000057"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 51.920000000000002, 19.149999999999999, "بولندا", "Poland", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000058"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 39.399999999999999, -8.2200000000000006, "البرتغال", "Portugal", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000059"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 45.939999999999998, 24.969999999999999, "رومانيا", "Romania", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000060"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 61.520000000000003, 105.31999999999999, "روسيا", "Russia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000061"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 43.939999999999998, 12.460000000000001, "سان مارينو", "San Marino", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000062"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 44.020000000000003, 21.010000000000002, "صربيا", "Serbia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000063"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 48.670000000000002, 19.699999999999999, "سلوفاكيا", "Slovakia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000064"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 46.149999999999999, 15.0, "سلوفينيا", "Slovenia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000065"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 40.460000000000001, -3.75, "إسبانيا", "Spain", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000066"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 60.130000000000003, 18.640000000000001, "السويد", "Sweden", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000067"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 46.82, 8.2300000000000004, "سويسرا", "Switzerland", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000068"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 48.380000000000003, 31.170000000000002, "أوكرانيا", "Ukraine", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000069"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 55.380000000000003, -3.4399999999999999, "المملكة المتحدة", "United Kingdom", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000070"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 41.899999999999999, 12.449999999999999, "الفاتيكان", "Vatican City", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000071"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 33.939999999999998, 67.709999999999994, "أفغانستان", "Afghanistan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000072"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 40.07, 45.039999999999999, "أرمينيا", "Armenia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000073"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 40.140000000000001, 47.579999999999998, "أذربيجان", "Azerbaijan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000074"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 23.68, 90.359999999999999, "بنغلاديش", "Bangladesh", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000075"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 27.510000000000002, 90.430000000000007, "بوتان", "Bhutan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000076"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 4.54, 114.73, "بروناي", "Brunei", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000077"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 12.57, 104.98999999999999, "كمبوديا", "Cambodia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000078"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 35.859999999999999, 104.2, "الصين", "China", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000079"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 42.32, 43.359999999999999, "جورجيا", "Georgia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000080"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 20.59, 78.959999999999994, "الهند", "India", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000081"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -0.79000000000000004, 113.92, "إندونيسيا", "Indonesia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000082"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 36.200000000000003, 138.25, "اليابان", "Japan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000083"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 48.020000000000003, 66.920000000000002, "كازاخستان", "Kazakhstan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000084"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 41.200000000000003, 74.769999999999996, "قيرغيزستان", "Kyrgyzstan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000085"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 19.859999999999999, 102.5, "لاوس", "Laos", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000086"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 4.21, 101.98, "ماليزيا", "Malaysia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000087"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3.2000000000000002, 73.219999999999999, "المالديف", "Maldives", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000088"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 46.859999999999999, 103.84999999999999, "منغوليا", "Mongolia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000089"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 21.91, 95.959999999999994, "ميانمار", "Myanmar", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000090"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 28.390000000000001, 84.120000000000005, "نيبال", "Nepal", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000091"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 40.340000000000003, 127.51000000000001, "كوريا الشمالية", "North Korea", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000092"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 30.379999999999999, 69.349999999999994, "باكستان", "Pakistan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000093"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 12.880000000000001, 121.77, "الفلبين", "Philippines", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000094"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1.3500000000000001, 103.81999999999999, "سنغافورة", "Singapore", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000095"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 35.909999999999997, 127.77, "كوريا الجنوبية", "South Korea", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000096"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 7.8700000000000001, 80.769999999999996, "سريلانكا", "Sri Lanka", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000097"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 38.859999999999999, 71.280000000000001, "طاجيكستان", "Tajikistan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000098"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.869999999999999, 100.98999999999999, "تايلاند", "Thailand", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000099"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -8.8699999999999992, 125.73, "تيمور الشرقية", "Timor-Leste", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000100"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 38.969999999999999, 59.560000000000002, "تركمانستان", "Turkmenistan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000101"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 41.380000000000003, 64.590000000000003, "أوزبكستان", "Uzbekistan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000102"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 14.06, 108.28, "فيتنام", "Vietnam", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000104"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -11.199999999999999, 17.870000000000001, "أنغولا", "Angola", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000105"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 9.3100000000000005, 2.3199999999999998, "بنين", "Benin", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000106"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -22.329999999999998, 24.68, "بوتسوانا", "Botswana", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000107"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 12.24, -1.5600000000000001, "بوركينا فاسو", "Burkina Faso", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000108"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -3.3700000000000001, 29.920000000000002, "بوروندي", "Burundi", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000109"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 16.0, -24.010000000000002, "الرأس الأخضر", "Cabo Verde", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000110"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 7.3700000000000001, 12.35, "الكاميرون", "Cameroon", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 6.6100000000000003, 20.940000000000001, "جمهورية أفريقيا الوسطى", "Central African Republic", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000112"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.449999999999999, 18.73, "تشاد", "Chad", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000113"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -0.23000000000000001, 15.83, "الكونغو", "Congo (Brazzaville)", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000114"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -4.04, 21.760000000000002, "الكونغو الديمقراطية", "Congo (Kinshasa)", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000115"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 7.54, -5.5499999999999998, "ساحل العاج", "Côte d'Ivoire", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000116"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1.6499999999999999, 10.27, "غينيا الاستوائية", "Equatorial Guinea", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000117"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.18, 39.780000000000001, "إريتريا", "Eritrea", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000118"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -26.52, 31.469999999999999, "إسواتيني", "Eswatini", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000119"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 9.1500000000000004, 40.490000000000002, "إثيوبيا", "Ethiopia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000120"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -0.80000000000000004, 11.609999999999999, "الغابون", "Gabon", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000121"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 13.44, -15.31, "غامبيا", "Gambia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000122"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 7.9500000000000002, -1.02, "غانا", "Ghana", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000123"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 9.9499999999999993, -9.6999999999999993, "غينيا", "Guinea", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000124"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 11.800000000000001, -15.18, "غينيا بيساو", "Guinea-Bissau", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000125"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -0.02, 37.909999999999997, "كينيا", "Kenya", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000126"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -29.609999999999999, 28.23, "ليسوتو", "Lesotho", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000127"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 6.4299999999999997, -9.4299999999999997, "ليبيريا", "Liberia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000128"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -18.77, 46.869999999999997, "مدغشقر", "Madagascar", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000129"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -13.25, 34.299999999999997, "مالاوي", "Malawi", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000130"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 17.57, -4.0, "مالي", "Mali", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000131"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -20.350000000000001, 57.549999999999997, "موريشيوس", "Mauritius", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000132"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -18.670000000000002, 35.530000000000001, "موزمبيق", "Mozambique", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000133"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -22.960000000000001, 18.489999999999998, "ناميبيا", "Namibia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000134"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 17.609999999999999, 8.0800000000000001, "النيجر", "Niger", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000135"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 9.0800000000000001, 8.6799999999999997, "نيجيريا", "Nigeria", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000136"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -1.9399999999999999, 29.870000000000001, "رواندا", "Rwanda", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000137"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 0.19, 6.6100000000000003, "ساو تومي وبرينسيبي", "São Tomé and Príncipe", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000138"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 14.5, -14.449999999999999, "السنغال", "Senegal", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000139"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -4.6799999999999997, 55.490000000000002, "سيشل", "Seychelles", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000140"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 8.4600000000000009, -11.779999999999999, "سيراليون", "Sierra Leone", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000141"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -30.559999999999999, 22.940000000000001, "جنوب أفريقيا", "South Africa", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000142"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 6.8799999999999999, 31.309999999999999, "جنوب السودان", "South Sudan", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000143"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -6.3700000000000001, 34.890000000000001, "تنزانيا", "Tanzania", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000144"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 8.6199999999999992, 0.81999999999999995, "توغو", "Togo", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000145"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 1.3700000000000001, 32.289999999999999, "أوغندا", "Uganda", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000146"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -13.130000000000001, 27.850000000000001, "زامبيا", "Zambia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000147"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -19.02, 29.149999999999999, "زيمبابوي", "Zimbabwe", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000148"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 17.059999999999999, -61.799999999999997, "أنتيغوا وباربودا", "Antigua and Barbuda", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000149"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -38.420000000000002, -63.619999999999997, "الأرجنتين", "Argentina", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000150"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 25.030000000000001, -77.400000000000006, "الباهاماس", "Bahamas", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000151"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 13.19, -59.539999999999999, "باربادوس", "Barbados", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000152"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 17.190000000000001, -88.5, "بليز", "Belize", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000153"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -16.289999999999999, -63.590000000000003, "بوليفيا", "Bolivia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000154"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -14.24, -51.93, "البرازيل", "Brazil", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000155"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 56.130000000000003, -106.34999999999999, "كندا", "Canada", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000156"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -35.68, -71.540000000000006, "تشيلي", "Chile", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000157"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 4.5700000000000003, -74.299999999999997, "كولومبيا", "Colombia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000158"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 9.75, -83.75, "كوستاريكا", "Costa Rica", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000159"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 21.52, -77.780000000000001, "كوبا", "Cuba", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000160"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.41, -61.369999999999997, "دومينيكا", "Dominica", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000161"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 18.739999999999998, -70.159999999999997, "جمهورية الدومينيكان", "Dominican Republic", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000162"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -1.8300000000000001, -78.180000000000007, "الإكوادور", "Ecuador", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000163"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 13.789999999999999, -88.900000000000006, "السلفادور", "El Salvador", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000164"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 12.26, -61.600000000000001, "غرينادا", "Grenada", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000165"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.779999999999999, -90.230000000000004, "غواتيمالا", "Guatemala", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000166"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 4.8600000000000003, -58.93, "غيانا", "Guyana", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000167"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 18.969999999999999, -72.290000000000006, "هايتي", "Haiti", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000168"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 15.199999999999999, -86.239999999999995, "هندوراس", "Honduras", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000169"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 18.109999999999999, -77.299999999999997, "جامايكا", "Jamaica", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000170"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 23.629999999999999, -102.55, "المكسيك", "Mexico", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000171"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 12.869999999999999, -85.209999999999994, "نيكاراغوا", "Nicaragua", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000172"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 8.5399999999999991, -80.780000000000001, "بنما", "Panama", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000173"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -23.440000000000001, -58.439999999999998, "باراغواي", "Paraguay", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000174"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -9.1899999999999995, -75.019999999999996, "بيرو", "Peru", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000175"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 17.359999999999999, -62.780000000000001, "سانت كيتس ونيفيس", "Saint Kitts and Nevis", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000176"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 13.91, -60.979999999999997, "سانت لوسيا", "Saint Lucia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000177"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 12.98, -61.289999999999999, "سانت فينسنت والغرينادين", "Saint Vincent and the Grenadines", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000178"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 3.9199999999999999, -56.030000000000001, "سورينام", "Suriname", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000179"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 10.69, -61.219999999999999, "ترينيداد وتوباغو", "Trinidad and Tobago", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000180"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 37.090000000000003, -95.709999999999994, "الولايات المتحدة", "United States", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000181"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -32.520000000000003, -55.770000000000003, "أوروغواي", "Uruguay", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000182"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 6.4199999999999999, -66.590000000000003, "فنزويلا", "Venezuela", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000183"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -25.27, 133.78, "أستراليا", "Australia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000184"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -17.710000000000001, 178.06999999999999, "فيجي", "Fiji", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000185"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -3.3700000000000001, -168.72999999999999, "كيريباتي", "Kiribati", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000186"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 7.1299999999999999, 171.18000000000001, "جزر مارشال", "Marshall Islands", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000187"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 7.4299999999999997, 150.55000000000001, "ميكرونيزيا", "Micronesia", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000188"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -0.52000000000000002, 166.93000000000001, "ناورو", "Nauru", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000189"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -40.899999999999999, 174.88999999999999, "نيوزيلندا", "New Zealand", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000190"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 7.5099999999999998, 134.58000000000001, "بالاو", "Palau", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000191"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -6.3099999999999996, 143.96000000000001, "بابوا غينيا الجديدة", "Papua New Guinea", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000192"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -13.76, -172.09999999999999, "ساموا", "Samoa", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000193"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -9.6500000000000004, 160.16, "جزر سليمان", "Solomon Islands", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000194"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -21.18, -175.19999999999999, "تونغا", "Tonga", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000195"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -7.1100000000000003, 177.65000000000001, "توفالو", "Tuvalu", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000196"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, -15.380000000000001, 166.96000000000001, "فانواتو", "Vanuatu", null, null },
                    { new Guid("c0000000-0000-0000-0000-000000000197"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, 23.699999999999999, 120.95999999999999, "تايوان", "Taiwan", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000062"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000063"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000064"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000065"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000066"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000067"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000068"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000069"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000070"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000071"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000072"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000073"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000074"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000075"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000076"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000077"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000078"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000079"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000080"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000081"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000082"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000083"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000084"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000085"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000086"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000087"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000088"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000089"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000090"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000091"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000092"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000093"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000094"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000095"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000096"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000097"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000098"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000099"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000100"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000101"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000102"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000103"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000104"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000105"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000106"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000107"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000108"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000109"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000110"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000111"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000112"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000113"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000114"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000115"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000116"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000117"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000118"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000119"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000120"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000121"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000122"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000123"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000124"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000125"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000126"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000127"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000128"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000129"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000130"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000131"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000132"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000133"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000134"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000135"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000136"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000137"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000138"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000139"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000140"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000141"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000142"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000143"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000144"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000145"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000146"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000147"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000148"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000149"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000150"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000151"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000152"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000153"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000154"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000155"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000156"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000157"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000158"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000159"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000160"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000161"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000162"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000163"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000164"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000165"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000166"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000167"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000168"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000169"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000170"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000171"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000172"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000173"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000174"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000175"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000176"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000177"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000178"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000179"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000180"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000181"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000182"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000183"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000184"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000185"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000186"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000187"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000188"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000189"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000190"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000191"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000192"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000193"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000194"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000195"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000196"));

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: new Guid("c0000000-0000-0000-0000-000000000197"));
        }
    }
}
