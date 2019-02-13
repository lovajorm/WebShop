using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Dal.Migrations
{
    public partial class purschaseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PurschaseId",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurschaseId",
                table: "Orders");
        }
    }
}
