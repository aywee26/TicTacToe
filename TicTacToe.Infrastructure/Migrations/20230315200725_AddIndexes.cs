using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                table: "Players",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Games_CreatedAt",
                table: "Games",
                column: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_Name",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Games_CreatedAt",
                table: "Games");
        }
    }
}
