using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Dal.Migrations
{
    public partial class addedSsnToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Orders",
                newName: "City");

            migrationBuilder.AddColumn<string>(
                name: "Ssn",
                table: "Orders",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                column: "ImageUrl",
                value: "~/images/Clothes/jeans.jfif");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ssn",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Orders",
                newName: "State");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                column: "ImageUrl",
                value: "~/images/Clothes/jeans.jpg");
        }
    }
}
