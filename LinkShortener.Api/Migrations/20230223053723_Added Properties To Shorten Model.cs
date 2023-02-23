using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkShortener.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddedPropertiesToShortenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Links",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Links_OwnerId",
                table: "Links",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Users_OwnerId",
                table: "Links",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Users_OwnerId",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Links_OwnerId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Links");
        }
    }
}
