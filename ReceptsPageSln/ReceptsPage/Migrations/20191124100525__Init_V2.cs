using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceptsPage.Migrations
{
    public partial class _Init_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BarCategories",
                columns: table => new
                {
                    BarCategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarCategories", x => x.BarCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "BarArticles",
                columns: table => new
                {
                    BarArticleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    ImgGeneral = table.Column<byte[]>(nullable: true),
                    Star = table.Column<string>(nullable: true),
                    BarCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarArticles", x => x.BarArticleId);
                    table.ForeignKey(
                        name: "FK_BarArticles_BarCategories_BarCategoryId",
                        column: x => x.BarCategoryId,
                        principalTable: "BarCategories",
                        principalColumn: "BarCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BarCategories",
                columns: new[] { "BarCategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Ինչ եփել այսօր" },
                    { 2, "Տորթեր" },
                    { 3, "Թխվածքաբլիթներ" },
                    { 4, "Պիցաներ" },
                    { 5, "Աղցաններ" },
                    { 6, "Դիետաներ" },
                    { 7, "Առողջ սնունդ" },
                    { 8, "Օգտայկար Խորհուրդներ" },
                    { 9, "Նկարներ" },
                    { 10, "Վիդեոներ" },
                    { 11, "Ռեստորաններ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarArticles_BarCategoryId",
                table: "BarArticles",
                column: "BarCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarArticles");

            migrationBuilder.DropTable(
                name: "BarCategories");
        }
    }
}
