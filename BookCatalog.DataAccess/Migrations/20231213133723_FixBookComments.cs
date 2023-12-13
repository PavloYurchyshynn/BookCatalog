using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCatalog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixBookComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Comments_Commentid",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_Commentid",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Commentid",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "Bookid",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Bookid",
                table: "Comments",
                column: "Bookid");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Books_Bookid",
                table: "Comments",
                column: "Bookid",
                principalTable: "Books",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Books_Bookid",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_Bookid",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Bookid",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "Commentid",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_Commentid",
                table: "Books",
                column: "Commentid");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Comments_Commentid",
                table: "Books",
                column: "Commentid",
                principalTable: "Comments",
                principalColumn: "id");
        }
    }
}
