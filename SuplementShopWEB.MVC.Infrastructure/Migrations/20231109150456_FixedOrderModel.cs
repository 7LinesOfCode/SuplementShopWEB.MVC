using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuplementShopWEB.MVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountOfItems",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfItems",
                table: "Orders");
        }
    }
}
