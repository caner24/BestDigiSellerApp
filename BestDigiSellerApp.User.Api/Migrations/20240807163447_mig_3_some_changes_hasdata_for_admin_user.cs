using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestDigiSellerApp.User.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig_3_some_changes_hasdata_for_admin_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96b42de0-4626-447a-8916-04e0326f6c8b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c33bab16-7a80-4c43-b680-e02cc314af71", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "294b1b78-3268-4226-8e86-87c63a6868b0", 0, "a3c6d198-4657-40ac-857e-735ecf8d3098", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN24", "AQAAAAIAAYagAAAAEAebH5s276vDLw4AY39y7qQhT77DaChPdzDfIhN0cGA1mKQUcUUpdwLrENESWE4taQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a4107b4a-6b3c-46be-a1e2-33805c5dc03f", false, "admin24" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c33bab16-7a80-4c43-b680-e02cc314af71", "294b1b78-3268-4226-8e86-87c63a6868b0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c33bab16-7a80-4c43-b680-e02cc314af71", "294b1b78-3268-4226-8e86-87c63a6868b0" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c33bab16-7a80-4c43-b680-e02cc314af71");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "294b1b78-3268-4226-8e86-87c63a6868b0");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "96b42de0-4626-447a-8916-04e0326f6c8b", 0, "9463cb7d-91bb-4d44-9952-82d7f3cfc914", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN24", "AQAAAAIAAYagAAAAEAZTnoxZ+t3UUqsWnvRyoxZ3jqiBzBm845X4GbLoCGWDTTLMj2nUX1rgUvPMHAuJYw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "530c6af2-8924-413c-bd75-f29e4f851e03", false, "admin24" });
        }
    }
}
