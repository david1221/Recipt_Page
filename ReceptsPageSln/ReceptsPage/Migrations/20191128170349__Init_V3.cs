using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceptsPage.Migrations
{
    public partial class _Init_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Star",
                table: "Articles");

            migrationBuilder.AddColumn<bool>(
                name: "IfFavorite",
                table: "Articles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IfFavorite",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "Star",
                table: "Articles",
                nullable: true);
        }
    }
}
