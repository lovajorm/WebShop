using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Dal.Migrations
{
    public partial class addednewdatawebShopDBContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/images/Clothes/svart.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "ImageUrl",
                value: "~/images/Clothes/byx.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "ImageUrl",
                value: "~/images/Clothes/hoodie.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4,
                column: "ImageUrl",
                value: "~/images/Clothes/kjol.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5,
                column: "ImageUrl",
                value: "~/images/Clothes/kofta.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                column: "ImageUrl",
                value: "~/images/Clothes/jeans.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7,
                column: "ImageUrl",
                value: "~/images/Clothes/tshirt.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8,
                column: "ImageUrl",
                value: "~/images/Clothes/blus.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9,
                column: "ImageUrl",
                value: "~/images/Clothes/shorts.jpg");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 10, 3, "Laptop from HP", "~/images/Electronics/laptop.jfif", 10000f, "Laptop" },
                    { 11, 2, "Dinner table", "~/images/Furniture/table.jpg", 2000f, "Table" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 11);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/images/svart.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "ImageUrl",
                value: "~/images/byx.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                column: "ImageUrl",
                value: "~/images/hoodie.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4,
                column: "ImageUrl",
                value: "~/images/kjol.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5,
                column: "ImageUrl",
                value: "~/images/kofta.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                column: "ImageUrl",
                value: "~/images/jeans.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7,
                column: "ImageUrl",
                value: "~/images/tshirt.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8,
                column: "ImageUrl",
                value: "~/images/blus.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 9,
                column: "ImageUrl",
                value: "~/images/shorts.jpg");
        }
    }
}
