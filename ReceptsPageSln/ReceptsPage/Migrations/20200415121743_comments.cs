using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceptsPage.Migrations
{
    public partial class comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "MainComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    appUserId = table.Column<int>(nullable: true),
                    articlePArticleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainComment_AspNetUsers_appUserId",
                        column: x => x.appUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainComment_Articles_articlePArticleId",
                        column: x => x.articlePArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChildComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    appUserId = table.Column<int>(nullable: true),
                    mainCommentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildComments_AspNetUsers_appUserId",
                        column: x => x.appUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChildComments_MainComment_mainCommentId",
                        column: x => x.mainCommentId,
                        principalTable: "MainComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });






            migrationBuilder.CreateIndex(
                name: "IX_ChildComments_appUserId",
                table: "ChildComments",
                column: "appUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildComments_mainCommentId",
                table: "ChildComments",
                column: "mainCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_MainComment_appUserId",
                table: "MainComment",
                column: "appUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainComment_articlePArticleId",
                table: "MainComment",
                column: "articlePArticleId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
