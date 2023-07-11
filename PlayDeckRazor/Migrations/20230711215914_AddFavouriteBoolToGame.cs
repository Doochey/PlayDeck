using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayDeckRazor.Migrations
{
    public partial class AddFavouriteBoolToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favourite",
                table: "Game",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favourite",
                table: "Game");
        }
    }
}
