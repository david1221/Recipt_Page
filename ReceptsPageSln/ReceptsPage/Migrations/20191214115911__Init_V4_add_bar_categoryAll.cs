using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceptsPage.Migrations
{
    public partial class _Init_V4_add_bar_categoryAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BarCategories",
                columns: new[] { "BarCategoryId", "Name" },
                values: new object[] { 12, "Այլ" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarCategories",
                keyColumn: "BarCategoryId",
                keyValue: 12);
        }
    }
}
