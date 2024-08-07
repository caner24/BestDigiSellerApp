using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestDigiSellerApp.User.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig_2_hasdata_for_admin_user_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "96b42de0-4626-447a-8916-04e0326f6c8b", 0, "9463cb7d-91bb-4d44-9952-82d7f3cfc914", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN24", "AQAAAAIAAYagAAAAEAZTnoxZ+t3UUqsWnvRyoxZ3jqiBzBm845X4GbLoCGWDTTLMj2nUX1rgUvPMHAuJYw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "530c6af2-8924-413c-bd75-f29e4f851e03", false, "admin24" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96b42de0-4626-447a-8916-04e0326f6c8b");
        }
    }
}
