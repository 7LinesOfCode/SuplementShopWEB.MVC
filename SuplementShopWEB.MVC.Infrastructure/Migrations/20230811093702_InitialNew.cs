using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuplementShopWEB.MVC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Items");
        }
    }
}
