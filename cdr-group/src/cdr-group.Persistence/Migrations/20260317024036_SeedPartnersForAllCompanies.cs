using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedPartnersForAllCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-aabb-aabb-aabb-aabbccddeeff"),
                column: "NumberOfEmployees",
                value: 500);

            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "Id", "CityId", "CompanyId", "CompanyId1", "CreatedAt", "CreatedBy", "IsDeleted", "Status", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { new Guid("e0000000-0000-0000-0001-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000002"), new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000003"), new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000004"), new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000005"), new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000006"), new Guid("d0000000-0000-0000-0000-000000000006"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000007"), new Guid("d0000000-0000-0000-0000-000000000007"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000008"), new Guid("d0000000-0000-0000-0000-000000000008"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000009"), new Guid("d0000000-0000-0000-0000-000000000009"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000010"), new Guid("d0000000-0000-0000-0000-000000000010"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000011"), new Guid("d0000000-0000-0000-0000-000000000011"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000012"), new Guid("d0000000-0000-0000-0000-000000000012"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000013"), new Guid("d0000000-0000-0000-0000-000000000013"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000014"), new Guid("d0000000-0000-0000-0000-000000000014"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000015"), new Guid("d0000000-0000-0000-0000-000000000015"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000016"), new Guid("d0000000-0000-0000-0000-000000000016"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000017"), new Guid("d0000000-0000-0000-0000-000000000017"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000018"), new Guid("d0000000-0000-0000-0000-000000000018"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000019"), new Guid("d0000000-0000-0000-0000-000000000019"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000020"), new Guid("d0000000-0000-0000-0000-000000000020"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000021"), new Guid("d0000000-0000-0000-0000-000000000021"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000022"), new Guid("d0000000-0000-0000-0000-000000000022"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000023"), new Guid("d0000000-0000-0000-0000-000000000023"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000024"), new Guid("d0000000-0000-0000-0000-000000000024"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000025"), new Guid("d0000000-0000-0000-0000-000000000025"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000026"), new Guid("d0000000-0000-0000-0000-000000000026"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000027"), new Guid("d0000000-0000-0000-0000-000000000027"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000028"), new Guid("d0000000-0000-0000-0000-000000000028"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000029"), new Guid("d0000000-0000-0000-0000-000000000029"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000030"), new Guid("d0000000-0000-0000-0000-000000000030"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000031"), new Guid("d0000000-0000-0000-0000-000000000031"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000032"), new Guid("d0000000-0000-0000-0000-000000000032"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000033"), new Guid("d0000000-0000-0000-0000-000000000033"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000034"), new Guid("d0000000-0000-0000-0000-000000000034"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000035"), new Guid("d0000000-0000-0000-0000-000000000035"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000036"), new Guid("d0000000-0000-0000-0000-000000000036"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000037"), new Guid("d0000000-0000-0000-0000-000000000037"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000038"), new Guid("d0000000-0000-0000-0000-000000000038"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000039"), new Guid("d0000000-0000-0000-0000-000000000039"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000040"), new Guid("d0000000-0000-0000-0000-000000000040"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000041"), new Guid("d0000000-0000-0000-0000-000000000041"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000042"), new Guid("d0000000-0000-0000-0000-000000000042"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000043"), new Guid("d0000000-0000-0000-0000-000000000043"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000044"), new Guid("d0000000-0000-0000-0000-000000000044"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000045"), new Guid("d0000000-0000-0000-0000-000000000045"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000046"), new Guid("d0000000-0000-0000-0000-000000000046"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000047"), new Guid("d0000000-0000-0000-0000-000000000047"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000048"), new Guid("d0000000-0000-0000-0000-000000000048"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000049"), new Guid("d0000000-0000-0000-0000-000000000049"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000050"), new Guid("d0000000-0000-0000-0000-000000000050"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000051"), new Guid("d0000000-0000-0000-0000-000000000051"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000052"), new Guid("d0000000-0000-0000-0000-000000000052"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000053"), new Guid("d0000000-0000-0000-0000-000000000053"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000054"), new Guid("d0000000-0000-0000-0000-000000000054"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000055"), new Guid("d0000000-0000-0000-0000-000000000055"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000056"), new Guid("d0000000-0000-0000-0000-000000000056"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000057"), new Guid("d0000000-0000-0000-0000-000000000057"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000058"), new Guid("d0000000-0000-0000-0000-000000000058"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000059"), new Guid("d0000000-0000-0000-0000-000000000059"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000060"), new Guid("d0000000-0000-0000-0000-000000000060"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000061"), new Guid("d0000000-0000-0000-0000-000000000061"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000062"), new Guid("d0000000-0000-0000-0000-000000000062"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000063"), new Guid("d0000000-0000-0000-0000-000000000063"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000064"), new Guid("d0000000-0000-0000-0000-000000000064"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000065"), new Guid("d0000000-0000-0000-0000-000000000065"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000066"), new Guid("d0000000-0000-0000-0000-000000000066"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000067"), new Guid("d0000000-0000-0000-0000-000000000067"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000068"), new Guid("d0000000-0000-0000-0000-000000000068"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000069"), new Guid("d0000000-0000-0000-0000-000000000069"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000070"), new Guid("d0000000-0000-0000-0000-000000000070"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0001-000000000071"), new Guid("d0000000-0000-0000-0000-000000000071"), new Guid("aabbccdd-aabb-aabb-aabb-000000000001"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000002"), new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000003"), new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000004"), new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000005"), new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000006"), new Guid("d0000000-0000-0000-0000-000000000006"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000007"), new Guid("d0000000-0000-0000-0000-000000000007"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000008"), new Guid("d0000000-0000-0000-0000-000000000008"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000009"), new Guid("d0000000-0000-0000-0000-000000000009"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000010"), new Guid("d0000000-0000-0000-0000-000000000010"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000011"), new Guid("d0000000-0000-0000-0000-000000000011"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000012"), new Guid("d0000000-0000-0000-0000-000000000012"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000013"), new Guid("d0000000-0000-0000-0000-000000000013"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000014"), new Guid("d0000000-0000-0000-0000-000000000014"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000015"), new Guid("d0000000-0000-0000-0000-000000000015"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000016"), new Guid("d0000000-0000-0000-0000-000000000016"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000017"), new Guid("d0000000-0000-0000-0000-000000000017"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000018"), new Guid("d0000000-0000-0000-0000-000000000018"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000019"), new Guid("d0000000-0000-0000-0000-000000000019"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000020"), new Guid("d0000000-0000-0000-0000-000000000020"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000021"), new Guid("d0000000-0000-0000-0000-000000000021"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000022"), new Guid("d0000000-0000-0000-0000-000000000022"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000023"), new Guid("d0000000-0000-0000-0000-000000000023"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000024"), new Guid("d0000000-0000-0000-0000-000000000024"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000025"), new Guid("d0000000-0000-0000-0000-000000000025"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000026"), new Guid("d0000000-0000-0000-0000-000000000026"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000027"), new Guid("d0000000-0000-0000-0000-000000000027"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000028"), new Guid("d0000000-0000-0000-0000-000000000028"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000029"), new Guid("d0000000-0000-0000-0000-000000000029"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000030"), new Guid("d0000000-0000-0000-0000-000000000030"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000031"), new Guid("d0000000-0000-0000-0000-000000000031"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000032"), new Guid("d0000000-0000-0000-0000-000000000032"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000033"), new Guid("d0000000-0000-0000-0000-000000000033"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000034"), new Guid("d0000000-0000-0000-0000-000000000034"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000035"), new Guid("d0000000-0000-0000-0000-000000000035"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000036"), new Guid("d0000000-0000-0000-0000-000000000036"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000037"), new Guid("d0000000-0000-0000-0000-000000000037"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000038"), new Guid("d0000000-0000-0000-0000-000000000038"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000039"), new Guid("d0000000-0000-0000-0000-000000000039"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000040"), new Guid("d0000000-0000-0000-0000-000000000040"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000041"), new Guid("d0000000-0000-0000-0000-000000000041"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000042"), new Guid("d0000000-0000-0000-0000-000000000042"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000043"), new Guid("d0000000-0000-0000-0000-000000000043"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000044"), new Guid("d0000000-0000-0000-0000-000000000044"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000045"), new Guid("d0000000-0000-0000-0000-000000000045"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000046"), new Guid("d0000000-0000-0000-0000-000000000046"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000047"), new Guid("d0000000-0000-0000-0000-000000000047"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000048"), new Guid("d0000000-0000-0000-0000-000000000048"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000049"), new Guid("d0000000-0000-0000-0000-000000000049"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000050"), new Guid("d0000000-0000-0000-0000-000000000050"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000051"), new Guid("d0000000-0000-0000-0000-000000000051"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000052"), new Guid("d0000000-0000-0000-0000-000000000052"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000053"), new Guid("d0000000-0000-0000-0000-000000000053"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000054"), new Guid("d0000000-0000-0000-0000-000000000054"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000055"), new Guid("d0000000-0000-0000-0000-000000000055"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000056"), new Guid("d0000000-0000-0000-0000-000000000056"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000057"), new Guid("d0000000-0000-0000-0000-000000000057"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000058"), new Guid("d0000000-0000-0000-0000-000000000058"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000059"), new Guid("d0000000-0000-0000-0000-000000000059"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000060"), new Guid("d0000000-0000-0000-0000-000000000060"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000061"), new Guid("d0000000-0000-0000-0000-000000000061"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000062"), new Guid("d0000000-0000-0000-0000-000000000062"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000063"), new Guid("d0000000-0000-0000-0000-000000000063"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000064"), new Guid("d0000000-0000-0000-0000-000000000064"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000065"), new Guid("d0000000-0000-0000-0000-000000000065"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000066"), new Guid("d0000000-0000-0000-0000-000000000066"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000067"), new Guid("d0000000-0000-0000-0000-000000000067"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000068"), new Guid("d0000000-0000-0000-0000-000000000068"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000069"), new Guid("d0000000-0000-0000-0000-000000000069"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000070"), new Guid("d0000000-0000-0000-0000-000000000070"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0002-000000000071"), new Guid("d0000000-0000-0000-0000-000000000071"), new Guid("aabbccdd-aabb-aabb-aabb-000000000002"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000002"), new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000003"), new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000004"), new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000005"), new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000006"), new Guid("d0000000-0000-0000-0000-000000000006"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000007"), new Guid("d0000000-0000-0000-0000-000000000007"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000008"), new Guid("d0000000-0000-0000-0000-000000000008"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000009"), new Guid("d0000000-0000-0000-0000-000000000009"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000010"), new Guid("d0000000-0000-0000-0000-000000000010"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000011"), new Guid("d0000000-0000-0000-0000-000000000011"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000012"), new Guid("d0000000-0000-0000-0000-000000000012"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000013"), new Guid("d0000000-0000-0000-0000-000000000013"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000014"), new Guid("d0000000-0000-0000-0000-000000000014"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000015"), new Guid("d0000000-0000-0000-0000-000000000015"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000016"), new Guid("d0000000-0000-0000-0000-000000000016"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000017"), new Guid("d0000000-0000-0000-0000-000000000017"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000018"), new Guid("d0000000-0000-0000-0000-000000000018"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000019"), new Guid("d0000000-0000-0000-0000-000000000019"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000020"), new Guid("d0000000-0000-0000-0000-000000000020"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000021"), new Guid("d0000000-0000-0000-0000-000000000021"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000022"), new Guid("d0000000-0000-0000-0000-000000000022"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000023"), new Guid("d0000000-0000-0000-0000-000000000023"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000024"), new Guid("d0000000-0000-0000-0000-000000000024"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000025"), new Guid("d0000000-0000-0000-0000-000000000025"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000026"), new Guid("d0000000-0000-0000-0000-000000000026"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000027"), new Guid("d0000000-0000-0000-0000-000000000027"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000028"), new Guid("d0000000-0000-0000-0000-000000000028"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000029"), new Guid("d0000000-0000-0000-0000-000000000029"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000030"), new Guid("d0000000-0000-0000-0000-000000000030"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000031"), new Guid("d0000000-0000-0000-0000-000000000031"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000032"), new Guid("d0000000-0000-0000-0000-000000000032"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000033"), new Guid("d0000000-0000-0000-0000-000000000033"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000034"), new Guid("d0000000-0000-0000-0000-000000000034"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000035"), new Guid("d0000000-0000-0000-0000-000000000035"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000036"), new Guid("d0000000-0000-0000-0000-000000000036"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000037"), new Guid("d0000000-0000-0000-0000-000000000037"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000038"), new Guid("d0000000-0000-0000-0000-000000000038"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000039"), new Guid("d0000000-0000-0000-0000-000000000039"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000040"), new Guid("d0000000-0000-0000-0000-000000000040"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000041"), new Guid("d0000000-0000-0000-0000-000000000041"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000042"), new Guid("d0000000-0000-0000-0000-000000000042"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000043"), new Guid("d0000000-0000-0000-0000-000000000043"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000044"), new Guid("d0000000-0000-0000-0000-000000000044"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000045"), new Guid("d0000000-0000-0000-0000-000000000045"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000046"), new Guid("d0000000-0000-0000-0000-000000000046"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000047"), new Guid("d0000000-0000-0000-0000-000000000047"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000048"), new Guid("d0000000-0000-0000-0000-000000000048"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000049"), new Guid("d0000000-0000-0000-0000-000000000049"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000050"), new Guid("d0000000-0000-0000-0000-000000000050"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000051"), new Guid("d0000000-0000-0000-0000-000000000051"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000052"), new Guid("d0000000-0000-0000-0000-000000000052"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000053"), new Guid("d0000000-0000-0000-0000-000000000053"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000054"), new Guid("d0000000-0000-0000-0000-000000000054"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000055"), new Guid("d0000000-0000-0000-0000-000000000055"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000056"), new Guid("d0000000-0000-0000-0000-000000000056"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000057"), new Guid("d0000000-0000-0000-0000-000000000057"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000058"), new Guid("d0000000-0000-0000-0000-000000000058"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000059"), new Guid("d0000000-0000-0000-0000-000000000059"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000060"), new Guid("d0000000-0000-0000-0000-000000000060"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000061"), new Guid("d0000000-0000-0000-0000-000000000061"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000062"), new Guid("d0000000-0000-0000-0000-000000000062"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000063"), new Guid("d0000000-0000-0000-0000-000000000063"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000064"), new Guid("d0000000-0000-0000-0000-000000000064"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000065"), new Guid("d0000000-0000-0000-0000-000000000065"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000066"), new Guid("d0000000-0000-0000-0000-000000000066"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000067"), new Guid("d0000000-0000-0000-0000-000000000067"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000068"), new Guid("d0000000-0000-0000-0000-000000000068"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000069"), new Guid("d0000000-0000-0000-0000-000000000069"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000070"), new Guid("d0000000-0000-0000-0000-000000000070"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0003-000000000071"), new Guid("d0000000-0000-0000-0000-000000000071"), new Guid("aabbccdd-aabb-aabb-aabb-000000000003"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000002"), new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000003"), new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000004"), new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000005"), new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000006"), new Guid("d0000000-0000-0000-0000-000000000006"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000007"), new Guid("d0000000-0000-0000-0000-000000000007"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000008"), new Guid("d0000000-0000-0000-0000-000000000008"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000009"), new Guid("d0000000-0000-0000-0000-000000000009"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000010"), new Guid("d0000000-0000-0000-0000-000000000010"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000011"), new Guid("d0000000-0000-0000-0000-000000000011"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000012"), new Guid("d0000000-0000-0000-0000-000000000012"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000013"), new Guid("d0000000-0000-0000-0000-000000000013"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000014"), new Guid("d0000000-0000-0000-0000-000000000014"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000015"), new Guid("d0000000-0000-0000-0000-000000000015"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000016"), new Guid("d0000000-0000-0000-0000-000000000016"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000017"), new Guid("d0000000-0000-0000-0000-000000000017"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000018"), new Guid("d0000000-0000-0000-0000-000000000018"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000019"), new Guid("d0000000-0000-0000-0000-000000000019"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000020"), new Guid("d0000000-0000-0000-0000-000000000020"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000021"), new Guid("d0000000-0000-0000-0000-000000000021"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000022"), new Guid("d0000000-0000-0000-0000-000000000022"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000023"), new Guid("d0000000-0000-0000-0000-000000000023"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000024"), new Guid("d0000000-0000-0000-0000-000000000024"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000025"), new Guid("d0000000-0000-0000-0000-000000000025"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000026"), new Guid("d0000000-0000-0000-0000-000000000026"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000027"), new Guid("d0000000-0000-0000-0000-000000000027"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000028"), new Guid("d0000000-0000-0000-0000-000000000028"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000029"), new Guid("d0000000-0000-0000-0000-000000000029"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000030"), new Guid("d0000000-0000-0000-0000-000000000030"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000031"), new Guid("d0000000-0000-0000-0000-000000000031"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000032"), new Guid("d0000000-0000-0000-0000-000000000032"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000033"), new Guid("d0000000-0000-0000-0000-000000000033"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000034"), new Guid("d0000000-0000-0000-0000-000000000034"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000035"), new Guid("d0000000-0000-0000-0000-000000000035"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000036"), new Guid("d0000000-0000-0000-0000-000000000036"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000037"), new Guid("d0000000-0000-0000-0000-000000000037"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000038"), new Guid("d0000000-0000-0000-0000-000000000038"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000039"), new Guid("d0000000-0000-0000-0000-000000000039"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000040"), new Guid("d0000000-0000-0000-0000-000000000040"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000041"), new Guid("d0000000-0000-0000-0000-000000000041"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000042"), new Guid("d0000000-0000-0000-0000-000000000042"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000043"), new Guid("d0000000-0000-0000-0000-000000000043"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000044"), new Guid("d0000000-0000-0000-0000-000000000044"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000045"), new Guid("d0000000-0000-0000-0000-000000000045"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000046"), new Guid("d0000000-0000-0000-0000-000000000046"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000047"), new Guid("d0000000-0000-0000-0000-000000000047"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000048"), new Guid("d0000000-0000-0000-0000-000000000048"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000049"), new Guid("d0000000-0000-0000-0000-000000000049"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000050"), new Guid("d0000000-0000-0000-0000-000000000050"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000051"), new Guid("d0000000-0000-0000-0000-000000000051"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000052"), new Guid("d0000000-0000-0000-0000-000000000052"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000053"), new Guid("d0000000-0000-0000-0000-000000000053"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000054"), new Guid("d0000000-0000-0000-0000-000000000054"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000055"), new Guid("d0000000-0000-0000-0000-000000000055"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000056"), new Guid("d0000000-0000-0000-0000-000000000056"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000057"), new Guid("d0000000-0000-0000-0000-000000000057"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000058"), new Guid("d0000000-0000-0000-0000-000000000058"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000059"), new Guid("d0000000-0000-0000-0000-000000000059"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000060"), new Guid("d0000000-0000-0000-0000-000000000060"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000061"), new Guid("d0000000-0000-0000-0000-000000000061"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000062"), new Guid("d0000000-0000-0000-0000-000000000062"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000063"), new Guid("d0000000-0000-0000-0000-000000000063"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000064"), new Guid("d0000000-0000-0000-0000-000000000064"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000065"), new Guid("d0000000-0000-0000-0000-000000000065"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000066"), new Guid("d0000000-0000-0000-0000-000000000066"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000067"), new Guid("d0000000-0000-0000-0000-000000000067"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000068"), new Guid("d0000000-0000-0000-0000-000000000068"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000069"), new Guid("d0000000-0000-0000-0000-000000000069"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000070"), new Guid("d0000000-0000-0000-0000-000000000070"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0004-000000000071"), new Guid("d0000000-0000-0000-0000-000000000071"), new Guid("aabbccdd-aabb-aabb-aabb-000000000004"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000002"), new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000003"), new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000004"), new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000005"), new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000006"), new Guid("d0000000-0000-0000-0000-000000000006"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000007"), new Guid("d0000000-0000-0000-0000-000000000007"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000008"), new Guid("d0000000-0000-0000-0000-000000000008"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000009"), new Guid("d0000000-0000-0000-0000-000000000009"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000010"), new Guid("d0000000-0000-0000-0000-000000000010"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000011"), new Guid("d0000000-0000-0000-0000-000000000011"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000012"), new Guid("d0000000-0000-0000-0000-000000000012"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000013"), new Guid("d0000000-0000-0000-0000-000000000013"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000014"), new Guid("d0000000-0000-0000-0000-000000000014"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000015"), new Guid("d0000000-0000-0000-0000-000000000015"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000016"), new Guid("d0000000-0000-0000-0000-000000000016"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000017"), new Guid("d0000000-0000-0000-0000-000000000017"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000018"), new Guid("d0000000-0000-0000-0000-000000000018"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000019"), new Guid("d0000000-0000-0000-0000-000000000019"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000020"), new Guid("d0000000-0000-0000-0000-000000000020"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000021"), new Guid("d0000000-0000-0000-0000-000000000021"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000022"), new Guid("d0000000-0000-0000-0000-000000000022"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000023"), new Guid("d0000000-0000-0000-0000-000000000023"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000024"), new Guid("d0000000-0000-0000-0000-000000000024"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000025"), new Guid("d0000000-0000-0000-0000-000000000025"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000026"), new Guid("d0000000-0000-0000-0000-000000000026"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000027"), new Guid("d0000000-0000-0000-0000-000000000027"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000028"), new Guid("d0000000-0000-0000-0000-000000000028"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000029"), new Guid("d0000000-0000-0000-0000-000000000029"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000030"), new Guid("d0000000-0000-0000-0000-000000000030"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000031"), new Guid("d0000000-0000-0000-0000-000000000031"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000032"), new Guid("d0000000-0000-0000-0000-000000000032"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000033"), new Guid("d0000000-0000-0000-0000-000000000033"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000034"), new Guid("d0000000-0000-0000-0000-000000000034"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000035"), new Guid("d0000000-0000-0000-0000-000000000035"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000036"), new Guid("d0000000-0000-0000-0000-000000000036"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000037"), new Guid("d0000000-0000-0000-0000-000000000037"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000038"), new Guid("d0000000-0000-0000-0000-000000000038"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000039"), new Guid("d0000000-0000-0000-0000-000000000039"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000040"), new Guid("d0000000-0000-0000-0000-000000000040"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000041"), new Guid("d0000000-0000-0000-0000-000000000041"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000042"), new Guid("d0000000-0000-0000-0000-000000000042"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000043"), new Guid("d0000000-0000-0000-0000-000000000043"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000044"), new Guid("d0000000-0000-0000-0000-000000000044"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000045"), new Guid("d0000000-0000-0000-0000-000000000045"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000046"), new Guid("d0000000-0000-0000-0000-000000000046"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000047"), new Guid("d0000000-0000-0000-0000-000000000047"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000048"), new Guid("d0000000-0000-0000-0000-000000000048"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000049"), new Guid("d0000000-0000-0000-0000-000000000049"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000050"), new Guid("d0000000-0000-0000-0000-000000000050"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000051"), new Guid("d0000000-0000-0000-0000-000000000051"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000052"), new Guid("d0000000-0000-0000-0000-000000000052"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000053"), new Guid("d0000000-0000-0000-0000-000000000053"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000054"), new Guid("d0000000-0000-0000-0000-000000000054"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000055"), new Guid("d0000000-0000-0000-0000-000000000055"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000056"), new Guid("d0000000-0000-0000-0000-000000000056"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000057"), new Guid("d0000000-0000-0000-0000-000000000057"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000058"), new Guid("d0000000-0000-0000-0000-000000000058"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000059"), new Guid("d0000000-0000-0000-0000-000000000059"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000060"), new Guid("d0000000-0000-0000-0000-000000000060"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000061"), new Guid("d0000000-0000-0000-0000-000000000061"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000062"), new Guid("d0000000-0000-0000-0000-000000000062"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000063"), new Guid("d0000000-0000-0000-0000-000000000063"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000064"), new Guid("d0000000-0000-0000-0000-000000000064"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000065"), new Guid("d0000000-0000-0000-0000-000000000065"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000066"), new Guid("d0000000-0000-0000-0000-000000000066"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000067"), new Guid("d0000000-0000-0000-0000-000000000067"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000068"), new Guid("d0000000-0000-0000-0000-000000000068"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000069"), new Guid("d0000000-0000-0000-0000-000000000069"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000070"), new Guid("d0000000-0000-0000-0000-000000000070"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0005-000000000071"), new Guid("d0000000-0000-0000-0000-000000000071"), new Guid("aabbccdd-aabb-aabb-aabb-000000000005"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000002"), new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000003"), new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000004"), new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000005"), new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000006"), new Guid("d0000000-0000-0000-0000-000000000006"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000007"), new Guid("d0000000-0000-0000-0000-000000000007"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000008"), new Guid("d0000000-0000-0000-0000-000000000008"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000009"), new Guid("d0000000-0000-0000-0000-000000000009"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000010"), new Guid("d0000000-0000-0000-0000-000000000010"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000011"), new Guid("d0000000-0000-0000-0000-000000000011"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000012"), new Guid("d0000000-0000-0000-0000-000000000012"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000013"), new Guid("d0000000-0000-0000-0000-000000000013"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000014"), new Guid("d0000000-0000-0000-0000-000000000014"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000015"), new Guid("d0000000-0000-0000-0000-000000000015"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000016"), new Guid("d0000000-0000-0000-0000-000000000016"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000017"), new Guid("d0000000-0000-0000-0000-000000000017"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000018"), new Guid("d0000000-0000-0000-0000-000000000018"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000019"), new Guid("d0000000-0000-0000-0000-000000000019"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000020"), new Guid("d0000000-0000-0000-0000-000000000020"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000021"), new Guid("d0000000-0000-0000-0000-000000000021"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000022"), new Guid("d0000000-0000-0000-0000-000000000022"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000023"), new Guid("d0000000-0000-0000-0000-000000000023"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000024"), new Guid("d0000000-0000-0000-0000-000000000024"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000025"), new Guid("d0000000-0000-0000-0000-000000000025"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000026"), new Guid("d0000000-0000-0000-0000-000000000026"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000027"), new Guid("d0000000-0000-0000-0000-000000000027"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000028"), new Guid("d0000000-0000-0000-0000-000000000028"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000029"), new Guid("d0000000-0000-0000-0000-000000000029"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000030"), new Guid("d0000000-0000-0000-0000-000000000030"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000031"), new Guid("d0000000-0000-0000-0000-000000000031"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000032"), new Guid("d0000000-0000-0000-0000-000000000032"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000033"), new Guid("d0000000-0000-0000-0000-000000000033"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000034"), new Guid("d0000000-0000-0000-0000-000000000034"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000035"), new Guid("d0000000-0000-0000-0000-000000000035"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000036"), new Guid("d0000000-0000-0000-0000-000000000036"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000037"), new Guid("d0000000-0000-0000-0000-000000000037"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000038"), new Guid("d0000000-0000-0000-0000-000000000038"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000039"), new Guid("d0000000-0000-0000-0000-000000000039"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000040"), new Guid("d0000000-0000-0000-0000-000000000040"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000041"), new Guid("d0000000-0000-0000-0000-000000000041"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000042"), new Guid("d0000000-0000-0000-0000-000000000042"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000043"), new Guid("d0000000-0000-0000-0000-000000000043"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000044"), new Guid("d0000000-0000-0000-0000-000000000044"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000045"), new Guid("d0000000-0000-0000-0000-000000000045"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000046"), new Guid("d0000000-0000-0000-0000-000000000046"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000047"), new Guid("d0000000-0000-0000-0000-000000000047"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000048"), new Guid("d0000000-0000-0000-0000-000000000048"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000049"), new Guid("d0000000-0000-0000-0000-000000000049"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000050"), new Guid("d0000000-0000-0000-0000-000000000050"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000051"), new Guid("d0000000-0000-0000-0000-000000000051"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000052"), new Guid("d0000000-0000-0000-0000-000000000052"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000053"), new Guid("d0000000-0000-0000-0000-000000000053"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000054"), new Guid("d0000000-0000-0000-0000-000000000054"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000055"), new Guid("d0000000-0000-0000-0000-000000000055"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000056"), new Guid("d0000000-0000-0000-0000-000000000056"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000057"), new Guid("d0000000-0000-0000-0000-000000000057"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000058"), new Guid("d0000000-0000-0000-0000-000000000058"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000059"), new Guid("d0000000-0000-0000-0000-000000000059"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000060"), new Guid("d0000000-0000-0000-0000-000000000060"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000061"), new Guid("d0000000-0000-0000-0000-000000000061"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000062"), new Guid("d0000000-0000-0000-0000-000000000062"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000063"), new Guid("d0000000-0000-0000-0000-000000000063"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000064"), new Guid("d0000000-0000-0000-0000-000000000064"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000065"), new Guid("d0000000-0000-0000-0000-000000000065"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000066"), new Guid("d0000000-0000-0000-0000-000000000066"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000067"), new Guid("d0000000-0000-0000-0000-000000000067"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000068"), new Guid("d0000000-0000-0000-0000-000000000068"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000069"), new Guid("d0000000-0000-0000-0000-000000000069"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000070"), new Guid("d0000000-0000-0000-0000-000000000070"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null },
                    { new Guid("e0000000-0000-0000-0006-000000000071"), new Guid("d0000000-0000-0000-0000-000000000071"), new Guid("aabbccdd-aabb-aabb-aabb-000000000006"), null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, false, "Available", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000001"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000002"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000003"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000004"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000005"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000006"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000007"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000008"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000009"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000010"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000011"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000012"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000013"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000014"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000015"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000016"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000017"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000018"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000019"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000020"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000021"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000022"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000023"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000024"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000025"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000026"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000027"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000028"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000029"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000030"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000031"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000032"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000033"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000034"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000035"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000036"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000037"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000038"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000039"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000040"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000041"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000042"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000043"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000044"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000045"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000046"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000047"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000048"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000049"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000050"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000051"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000052"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000053"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000054"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000055"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000056"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000057"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000058"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000059"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000060"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000061"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000062"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000063"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000064"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000065"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000066"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000067"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000068"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000069"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000070"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0001-000000000071"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000001"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000002"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000003"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000004"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000005"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000006"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000007"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000008"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000009"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000010"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000011"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000012"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000013"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000014"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000015"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000016"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000017"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000018"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000019"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000020"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000021"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000022"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000023"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000024"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000025"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000026"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000027"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000028"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000029"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000030"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000031"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000032"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000033"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000034"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000035"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000036"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000037"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000038"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000039"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000040"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000041"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000042"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000043"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000044"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000045"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000046"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000047"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000048"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000049"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000050"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000051"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000052"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000053"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000054"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000055"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000056"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000057"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000058"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000059"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000060"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000061"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000062"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000063"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000064"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000065"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000066"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000067"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000068"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000069"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000070"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0002-000000000071"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000001"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000002"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000003"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000004"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000005"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000006"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000007"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000008"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000009"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000010"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000011"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000012"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000013"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000014"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000015"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000016"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000017"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000018"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000019"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000020"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000021"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000022"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000023"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000024"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000025"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000026"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000027"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000028"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000029"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000030"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000031"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000032"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000033"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000034"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000035"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000036"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000037"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000038"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000039"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000040"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000041"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000042"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000043"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000044"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000045"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000046"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000047"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000048"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000049"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000050"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000051"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000052"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000053"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000054"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000055"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000056"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000057"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000058"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000059"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000060"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000061"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000062"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000063"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000064"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000065"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000066"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000067"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000068"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000069"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000070"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0003-000000000071"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000001"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000002"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000003"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000004"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000005"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000006"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000007"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000008"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000009"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000010"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000011"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000012"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000013"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000014"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000015"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000016"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000017"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000018"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000019"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000020"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000021"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000022"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000023"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000024"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000025"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000026"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000027"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000028"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000029"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000030"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000031"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000032"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000033"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000034"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000035"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000036"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000037"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000038"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000039"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000040"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000041"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000042"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000043"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000044"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000045"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000046"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000047"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000048"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000049"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000050"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000051"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000052"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000053"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000054"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000055"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000056"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000057"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000058"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000059"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000060"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000061"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000062"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000063"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000064"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000065"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000066"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000067"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000068"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000069"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000070"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0004-000000000071"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000001"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000002"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000003"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000004"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000005"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000006"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000007"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000008"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000009"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000010"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000011"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000012"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000013"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000014"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000015"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000016"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000017"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000018"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000019"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000020"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000021"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000022"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000023"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000024"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000025"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000026"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000027"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000028"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000029"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000030"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000031"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000032"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000033"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000034"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000035"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000036"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000037"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000038"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000039"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000040"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000041"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000042"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000043"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000044"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000045"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000046"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000047"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000048"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000049"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000050"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000051"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000052"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000053"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000054"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000055"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000056"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000057"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000058"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000059"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000060"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000061"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000062"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000063"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000064"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000065"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000066"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000067"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000068"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000069"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000070"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0005-000000000071"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000001"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000002"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000003"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000004"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000005"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000006"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000007"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000008"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000009"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000010"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000011"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000012"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000013"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000014"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000015"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000016"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000017"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000018"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000019"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000020"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000021"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000022"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000023"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000024"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000025"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000026"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000027"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000028"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000029"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000030"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000031"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000032"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000033"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000034"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000035"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000036"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000037"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000038"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000039"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000040"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000041"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000042"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000043"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000044"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000045"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000046"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000047"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000048"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000049"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000050"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000051"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000052"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000053"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000054"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000055"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000056"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000057"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000058"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000059"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000060"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000061"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000062"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000063"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000064"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000065"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000066"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000067"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000068"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000069"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000070"));

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0006-000000000071"));

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("aabbccdd-aabb-aabb-aabb-aabbccddeeff"),
                column: "NumberOfEmployees",
                value: 0);
        }
    }
}
