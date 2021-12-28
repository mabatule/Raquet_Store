using Microsoft.EntityFrameworkCore.Migrations;

namespace RaquetShop.Migrations
{
    public partial class addImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainPhoto",
                table: "Raquets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondPhoto",
                table: "Raquets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPhoto",
                table: "Raquets");

            migrationBuilder.DropColumn(
                name: "SecondPhoto",
                table: "Raquets");
        }
    }
}
