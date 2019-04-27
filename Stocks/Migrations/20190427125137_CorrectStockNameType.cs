using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocks.Migrations
{
    public partial class CorrectStockNameType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stocks",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Stocks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
