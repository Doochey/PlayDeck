using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayDeckRazor.Migrations
{
    public partial class AddDeckID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeckID",
                table: "Game",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeckID",
                table: "Game");
        }
    }
}
