using Microsoft.EntityFrameworkCore.Migrations;

namespace PointSystem.Data.Migrations
{
    public partial class UrlStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RediarectFlag",
                table: "Feasts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RediarectUrl",
                table: "Feasts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RediarectFlag",
                table: "Feasts");

            migrationBuilder.DropColumn(
                name: "RediarectUrl",
                table: "Feasts");
        }
    }
}
