using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirst.Migrations
{
    public partial class CodeFirstAddClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentgENDER",
                table: "Students",
                newName: "StudentGender");

            migrationBuilder.AddColumn<int>(
                name: "Standard",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Standard",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "StudentGender",
                table: "Students",
                newName: "StudentgENDER");
        }
    }
}
