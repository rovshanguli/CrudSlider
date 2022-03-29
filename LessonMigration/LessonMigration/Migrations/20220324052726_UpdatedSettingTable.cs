using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LessonMigration.Migrations
{
    public partial class UpdatedSettingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "HomeProductTake",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "LoadTake",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Settings",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 3, 24, 9, 27, 25, 608, DateTimeKind.Local).AddTicks(5988));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 3, 24, 9, 27, 25, 610, DateTimeKind.Local).AddTicks(4165));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2022, 3, 24, 9, 27, 25, 610, DateTimeKind.Local).AddTicks(4238));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Settings");

            migrationBuilder.AddColumn<int>(
                name: "HomeProductTake",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LoadTake",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2022, 3, 17, 12, 56, 11, 976, DateTimeKind.Local).AddTicks(8662));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2022, 3, 17, 12, 56, 11, 980, DateTimeKind.Local).AddTicks(1740));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2022, 3, 17, 12, 56, 11, 980, DateTimeKind.Local).AddTicks(1834));

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "HomeProductTake", "LoadTake" },
                values: new object[] { 1, 8, 4 });
        }
    }
}
