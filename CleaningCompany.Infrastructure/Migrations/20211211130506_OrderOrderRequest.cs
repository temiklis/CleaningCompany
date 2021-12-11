using Microsoft.EntityFrameworkCore.Migrations;

namespace CleaningCompany.Infrastructure.Migrations
{
    public partial class OrderOrderRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderRequestId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderRequestId",
                table: "Orders",
                column: "OrderRequestId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderRequestId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderRequestId",
                table: "Orders",
                column: "OrderRequestId");
        }
    }
}
