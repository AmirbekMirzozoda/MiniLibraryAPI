using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniLibraryAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBookNewIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_Name_AuthorId_CategoryId",
                table: "Books",
                columns: new[] { "Name", "AuthorId", "CategoryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_Name_AuthorId_CategoryId",
                table: "Books");
        }
    }
}
