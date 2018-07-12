using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerce.Migrations
{
    public partial class seeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "Image", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { 1, "Make a squirrel who eats from this feeder look like it had a massive cat face", "https://cdn.shopify.com/s/files/1/1365/2497/products/Cat-Head-Squirrel-Feeder_1600x.jpg?v=1520533809", "Cat Head Squirrel Feeder", 14.99m, "MNAG0001" },
                    { 2, "A tin of mints featuring a squirrel in underpants. Keep your breathe nutty fresh.", "https://cdn.shopify.com/s/files/1/1365/2497/products/Squirrel-in-Underpants-Mints_1600x.jpg?v=1520533849", "Squirrel in Underpants Mints", 3.99m, "MNAG0002" },
                    { 3, "Ever wanted your hand to be a squirrel? Now you can with these finger puppets. Includes four paws and a squirrel head.", "https://cdn.shopify.com/s/files/1/1365/2497/products/handisquirrel-Box_1600x.jpg?v=1520533725", "Handisquirrel - Squirrel Finger Puppet", 6.99m, "MNAG0003" },
                    { 4, "Ever wanted your hand to be a squirrel? Now you can with these finger puppets. Includes four paws and a squirrel head.", "https://cdn.shopify.com/s/files/1/1365/2497/products/handisquirrel-Box_1600x.jpg?v=1520533725", "Handisquirrel - Squirrel Finger Puppet", 6.99m, "MNAG0004" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
