using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocks.Migrations
{
    public partial class References : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsStocksHistory",
                table: "ItemsStocksHistory");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ItemStates");

            migrationBuilder.AddColumn<int>(
                name: "ItemStateId",
                table: "Items",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsStocksHistory",
                table: "ItemsStocksHistory",
                columns: new[] { "ItemId", "StockId", "ItemStateId" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersStocks_StockId",
                table: "UsersStocks",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsStocksHistory_ItemStateId",
                table: "ItemsStocksHistory",
                column: "ItemStateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsStocksHistory_StockId",
                table: "ItemsStocksHistory",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemStateId",
                table: "Items",
                column: "ItemStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemStates_ItemStateId",
                table: "Items",
                column: "ItemStateId",
                principalTable: "ItemStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsStocksHistory_Items_ItemId",
                table: "ItemsStocksHistory",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsStocksHistory_ItemStates_ItemStateId",
                table: "ItemsStocksHistory",
                column: "ItemStateId",
                principalTable: "ItemStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsStocksHistory_Stocks_StockId",
                table: "ItemsStocksHistory",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStocks_Stocks_StockId",
                table: "UsersStocks",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersStocks_Users_UserId",
                table: "UsersStocks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemStates_ItemStateId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsStocksHistory_Items_ItemId",
                table: "ItemsStocksHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsStocksHistory_ItemStates_ItemStateId",
                table: "ItemsStocksHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsStocksHistory_Stocks_StockId",
                table: "ItemsStocksHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersStocks_Stocks_StockId",
                table: "UsersStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersStocks_Users_UserId",
                table: "UsersStocks");

            migrationBuilder.DropIndex(
                name: "IX_UsersStocks_StockId",
                table: "UsersStocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsStocksHistory",
                table: "ItemsStocksHistory");

            migrationBuilder.DropIndex(
                name: "IX_ItemsStocksHistory_ItemStateId",
                table: "ItemsStocksHistory");

            migrationBuilder.DropIndex(
                name: "IX_ItemsStocksHistory_StockId",
                table: "ItemsStocksHistory");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemStateId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ItemStateId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "ItemStates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsStocksHistory",
                table: "ItemsStocksHistory",
                columns: new[] { "ItemId", "StockId", "ArrivalDate" });
        }
    }
}
