using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerce.Migrations
{
    public partial class loginRetry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 7,
                column: "Image",
                value: "https://cdn.shopify.com/s/files/1/1365/2497/products/squirrel_in_underpants_ornament_1600x.jpg?v=1520535285");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 7,
                column: "Image",
                value: "https://cdn.shopify.com/s/files/1/1365/2497/products/handisquirrel-Box_1600x.jpg?v=1520533725");
        }
    }
}
