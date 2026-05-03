using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cdr_group.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ensuretitledescriptionrolepermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ensure Super Admin and Admin role-permission rows exist for all
            // four CompanyTitleDescription permissions. INSERT IGNORE relies on
            // the unique index on (RoleId, PermissionId) — any rows that already
            // exist are silently skipped.
            migrationBuilder.Sql(@"
                INSERT IGNORE INTO `RolePermissions` (`Id`, `RoleId`, `PermissionId`, `CreatedAt`, `IsDeleted`) VALUES
                (UUID(), '11111111-1111-1111-1111-111111111111', 'ccddeeff-3333-3333-3333-aaaaaaaaaaaa', UTC_TIMESTAMP(6), 0),
                (UUID(), '11111111-1111-1111-1111-111111111111', 'ccddeeff-3333-3333-3333-bbbbbbbbbbbb', UTC_TIMESTAMP(6), 0),
                (UUID(), '11111111-1111-1111-1111-111111111111', 'ccddeeff-3333-3333-3333-cccccccccccc', UTC_TIMESTAMP(6), 0),
                (UUID(), '11111111-1111-1111-1111-111111111111', 'ccddeeff-3333-3333-3333-dddddddddddd', UTC_TIMESTAMP(6), 0),
                (UUID(), '55555555-5555-5555-5555-555555555555', 'ccddeeff-3333-3333-3333-aaaaaaaaaaaa', UTC_TIMESTAMP(6), 0),
                (UUID(), '55555555-5555-5555-5555-555555555555', 'ccddeeff-3333-3333-3333-bbbbbbbbbbbb', UTC_TIMESTAMP(6), 0),
                (UUID(), '55555555-5555-5555-5555-555555555555', 'ccddeeff-3333-3333-3333-cccccccccccc', UTC_TIMESTAMP(6), 0),
                (UUID(), '55555555-5555-5555-5555-555555555555', 'ccddeeff-3333-3333-3333-dddddddddddd', UTC_TIMESTAMP(6), 0);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM `RolePermissions`
                WHERE `RoleId` IN (
                    '11111111-1111-1111-1111-111111111111',
                    '55555555-5555-5555-5555-555555555555'
                )
                AND `PermissionId` IN (
                    'ccddeeff-3333-3333-3333-aaaaaaaaaaaa',
                    'ccddeeff-3333-3333-3333-bbbbbbbbbbbb',
                    'ccddeeff-3333-3333-3333-cccccccccccc',
                    'ccddeeff-3333-3333-3333-dddddddddddd'
                );
            ");
        }
    }
}
