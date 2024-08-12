using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestDigiSellerApp.Product.Api.Migrations
{
    /// <inheritdoc />
    public partial class mig_4_some_changes_for_concurrency_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concurrency",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Concurrency",
                table: "Product",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
