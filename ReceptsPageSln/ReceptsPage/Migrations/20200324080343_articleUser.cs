using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceptsPage.Migrations
{
    public partial class articleUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "BarArticles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BarArticles_AppUserId",
                table: "BarArticles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AppUserId",
                table: "Articles",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BarArticles_AspNetUsers_AppUserId",
                table: "BarArticles",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_AppUserId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_BarArticles_AspNetUsers_AppUserId",
                table: "BarArticles");

            migrationBuilder.DropIndex(
                name: "IX_BarArticles_AppUserId",
                table: "BarArticles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_AppUserId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "BarArticles");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Articles");
        }
    }
}
