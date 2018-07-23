using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerce.Migrations
{
    public partial class ordertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "BasketItemTable",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderTable",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true),
                    BasketID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTable", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemTable_OrderID",
                table: "BasketItemTable",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItemTable_OrderTable_OrderID",
                table: "BasketItemTable",
                column: "OrderID",
                principalTable: "OrderTable",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItemTable_OrderTable_OrderID",
                table: "BasketItemTable");

            migrationBuilder.DropTable(
                name: "OrderTable");

            migrationBuilder.DropIndex(
                name: "IX_BasketItemTable_OrderID",
                table: "BasketItemTable");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "BasketItemTable");
        }
    }
}
