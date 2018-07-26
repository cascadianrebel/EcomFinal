using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerce.Migrations
{
    public partial class enumAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "State",
                table: "OrderTable",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreditCard",
                table: "OrderTable",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "OrderTable",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "OrderTable",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "OrderTable",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCard",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderTable");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "OrderTable",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
