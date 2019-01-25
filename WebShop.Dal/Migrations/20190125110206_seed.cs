using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Dal.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Beskrivning av produkt 1", "", 10f, "Produkt 1" },
                    { 2, "Beskrivning av objekt 2", "", 20f, "Produkt 2" },
                    { 3, "Beskrivning av objekt 3", "", 30f, "Produkt 3" },
                    { 4, "Beskrivning av objekt 4", "", 40f, "Produkt 4" },
                    { 5, "Beskrivning av objekt 5", "", 50f, "Produkt 5" },
                    { 6, "Beskrivning av objekt 6", "", 60f, "Produkt 6" },
                    { 7, "Beskrivning av objekt 7", "", 70f, "Produkt 7" },
                    { 8, "Beskrivning av objekt 8", "", 80f, "Produkt 8" },
                    { 9, "Beskrivning av objekt 9", "", 90f, "Produkt 9" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9);
        }
    }
}
