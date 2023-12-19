using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCatalog.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersItems_Books_BookId",
                table: "OrdersItems");

            migrationBuilder.DropIndex(
                name: "IX_OrdersItems_BookId",
                table: "OrdersItems");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrdersItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrdersItems");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersItems_BookId",
                table: "OrdersItems",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersItems_Books_BookId",
                table: "OrdersItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
