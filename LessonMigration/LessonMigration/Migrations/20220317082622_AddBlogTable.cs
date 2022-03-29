using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LessonMigration.Migrations
{
    public partial class AddBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Description", "Image", "IsDeleted", "Title" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blog1 desctiption", "blog-feature-img-1.jpg", false, "Blog1" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Description", "Image", "IsDeleted", "Title" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blog2 desctiption", "blog-feature-img-3.jpg", false, "Blog2" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "Date", "Description", "Image", "IsDeleted", "Title" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blog3 desctiption", "blog-feature-img-4.jpg", false, "Blog3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
