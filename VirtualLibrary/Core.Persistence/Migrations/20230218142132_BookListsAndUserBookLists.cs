using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BookListsAndUserBookLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookLists",
                columns: table => new
                {
                    BookListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookListName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLists", x => x.BookListId);
                });

            migrationBuilder.CreateTable(
                name: "UserBookLists",
                columns: table => new
                {
                    UserBookListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserBookListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBookLists", x => x.UserBookListId);
                    table.ForeignKey(
                        name: "FK_UserBookLists_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBookLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookLists",
                columns: new[] { "BookListId", "BookListName" },
                values: new object[,]
                {
                    { 1, "Want to read" },
                    { 2, "Currently reading" },
                    { 3, "Already read" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBookLists_BookId",
                table: "UserBookLists",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBookLists_UserId",
                table: "UserBookLists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLists");

            migrationBuilder.DropTable(
                name: "UserBookLists");
        }
    }
}
