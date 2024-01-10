using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookListsAndUserBookLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBookLists_Users_UserId",
                table: "UserBookLists");

            migrationBuilder.DeleteData(
                table: "BookLists",
                keyColumn: "BookListId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookLists",
                keyColumn: "BookListId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookLists",
                keyColumn: "BookListId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "UserBookListName",
                table: "UserBookLists");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserBookLists",
                newName: "BookListId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBookLists_UserId",
                table: "UserBookLists",
                newName: "IX_UserBookLists_BookListId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "BookLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookLists_UserId",
                table: "BookLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLists_Users_UserId",
                table: "BookLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookLists_BookLists_BookListId",
                table: "UserBookLists",
                column: "BookListId",
                principalTable: "BookLists",
                principalColumn: "BookListId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLists_Users_UserId",
                table: "BookLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBookLists_BookLists_BookListId",
                table: "UserBookLists");

            migrationBuilder.DropIndex(
                name: "IX_BookLists_UserId",
                table: "BookLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "BookLists");

            migrationBuilder.RenameColumn(
                name: "BookListId",
                table: "UserBookLists",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBookLists_BookListId",
                table: "UserBookLists",
                newName: "IX_UserBookLists_UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserBookListName",
                table: "UserBookLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "BookLists",
                columns: new[] { "BookListId", "BookListName" },
                values: new object[,]
                {
                    { 1, "Want to read" },
                    { 2, "Currently reading" },
                    { 3, "Already read" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserBookLists_Users_UserId",
                table: "UserBookLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
