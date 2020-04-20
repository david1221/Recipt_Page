using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceptsPage.Migrations
{
    public partial class bararticlecolums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildComments_AspNetUsers_appUserId",
                table: "ChildComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildComments_MainComment_mainCommentId",
                table: "ChildComments");

            migrationBuilder.AlterColumn<int>(
                name: "mainCommentId",
                table: "ChildComments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "appUserId",
                table: "ChildComments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AdminConfirm",
                table: "BarArticles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildComments_AspNetUsers_appUserId",
                table: "ChildComments",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildComments_MainComment_mainCommentId",
                table: "ChildComments",
                column: "mainCommentId",
                principalTable: "MainComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildComments_AspNetUsers_appUserId",
                table: "ChildComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildComments_MainComment_mainCommentId",
                table: "ChildComments");

            migrationBuilder.DropColumn(
                name: "AdminConfirm",
                table: "BarArticles");

            migrationBuilder.AlterColumn<int>(
                name: "mainCommentId",
                table: "ChildComments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "appUserId",
                table: "ChildComments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ChildComments_AspNetUsers_appUserId",
                table: "ChildComments",
                column: "appUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildComments_MainComment_mainCommentId",
                table: "ChildComments",
                column: "mainCommentId",
                principalTable: "MainComment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
