using Microsoft.EntityFrameworkCore.Migrations;

namespace LessonMigration.Migrations
{
    public partial class AddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XTake",
                table: "Settings");

            migrationBuilder.AddColumn<int>(
                name: "HomeProductTake",
                table: "Settings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "HomeProductTake", "LoadTake" },
                values: new object[] { 1, 8, 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "HomeProductTake",
                table: "Settings");

            migrationBuilder.AddColumn<int>(
                name: "XTake",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
