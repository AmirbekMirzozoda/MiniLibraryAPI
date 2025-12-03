using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniLibraryAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_PublishedDate",
                table: "Books",
                column: "PublishedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_PublishedDate",
                table: "Books");
        }
    }
}
