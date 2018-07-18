using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerce.Migrations
{
    public partial class productID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItemTable_Products_ProductID",
                table: "BasketItemTable");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "BasketItemTable",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItemTable_Products_ProductID",
                table: "BasketItemTable",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItemTable_Products_ProductID",
                table: "BasketItemTable");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "BasketItemTable",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItemTable_Products_ProductID",
                table: "BasketItemTable",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
