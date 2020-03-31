using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceptsPage.Migrations
{
    public partial class confirmAdminArticke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdminConfirm",
                table: "Articles",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminConfirm",
                table: "Articles");
        }
    }
}
