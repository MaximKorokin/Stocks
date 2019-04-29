using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocks.Migrations
{
    public partial class OptionalItemState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsStocksHistory_ItemStates_ItemStateId",
                table: "ItemsStocksHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsStocksHistory",
                table: "ItemsStocksHistory");

            migrationBuilder.DropIndex(
                name: "IX_ItemsStocksHistory_ItemStateId",
                table: "ItemsStocksHistory");

            migrationBuilder.AlterColumn<int>(
                name: "ItemStateId",
                table: "ItemsStocksHistory",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsStocksHistory",
                table: "ItemsStocksHistory",
                columns: new[] { "ItemId", "StockId", "ArrivalDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsStocksHistory_ItemStateId",
                table: "ItemsStocksHistory",
                column: "ItemStateId",
                unique: true,
                filter: "[ItemStateId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsStocksHistory_ItemStates_ItemStateId",
                table: "ItemsStocksHistory",
                column: "ItemStateId",
                principalTable: "ItemStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsStocksHistory_ItemStates_ItemStateId",
                table: "ItemsStocksHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsStocksHistory",
                table: "ItemsStocksHistory");

            migrationBuilder.DropIndex(
                name: "IX_ItemsStocksHistory_ItemStateId",
                table: "ItemsStocksHistory");

            migrationBuilder.AlterColumn<int>(
                name: "ItemStateId",
                table: "ItemsStocksHistory",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsStocksHistory",
                table: "ItemsStocksHistory",
                columns: new[] { "ItemId", "StockId", "ItemStateId" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsStocksHistory_ItemStateId",
                table: "ItemsStocksHistory",
                column: "ItemStateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsStocksHistory_ItemStates_ItemStateId",
                table: "ItemsStocksHistory",
                column: "ItemStateId",
                principalTable: "ItemStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
