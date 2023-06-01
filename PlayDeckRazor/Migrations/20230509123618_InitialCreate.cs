using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayDeckRazor.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlatformID = table.Column<int>(type: "int", nullable: true),
                    PlayStatus = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    PlayTime = table.Column<float>(type: "real", nullable: true),
                    LastPlayed = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Game_Platform_PlatformID",
                        column: x => x.PlatformID,
                        principalTable: "Platform",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlatformID",
                table: "Game",
                column: "PlatformID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Platform");
        }
    }
}
