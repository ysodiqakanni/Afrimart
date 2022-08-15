using Microsoft.EntityFrameworkCore.Migrations;

namespace Afrimart.DataAccess.Migrations
{
    public partial class ModifyStoreAndProductTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Stores",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "StoreLogoUri",
                table: "Stores",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalEarnings",
                table: "Stores",
                type: "decimal(5, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalReviewCount",
                table: "Stores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSalesCount",
                table: "Stores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AverageRating",
                table: "Products",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ReviewCount",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalesCount",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalEarnings",
                table: "Products",
                type: "decimal(5, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreLogoUri",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "TotalEarnings",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "TotalReviewCount",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "TotalSalesCount",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ReviewCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalesCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalEarnings",
                table: "Products");
        }
    }
}
