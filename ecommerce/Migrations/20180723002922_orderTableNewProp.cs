using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerce.Migrations
{
    public partial class orderTableNewProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "OrderTable",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "OrderTable",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderTable",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "OrderTable",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "OrderTable",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "OrderTable",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "State",
                table: "OrderTable");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "OrderTable");
        }
    }
}
