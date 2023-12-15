using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCatalog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Books_BookId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Comments",
                newName: "Bookid");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_BookId",
                table: "Comments",
                newName: "IX_Comments_Bookid");

            migrationBuilder.AlterColumn<Guid>(
                name: "Bookid",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.RenameColumn(
                name: "Bookid",
                table: "Comments",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_Bookid",
                table: "Comments",
                newName: "IX_Comments_BookId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BookId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Books_BookId",
                table: "Comments",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
