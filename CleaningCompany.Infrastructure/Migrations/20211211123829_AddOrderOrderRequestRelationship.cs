using Microsoft.EntityFrameworkCore.Migrations;

namespace CleaningCompany.Infrastructure.Migrations
{
    public partial class AddOrderOrderRequestRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderRequestId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderRequestId",
                table: "Orders",
                column: "OrderRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderRequests_OrderRequestId",
                table: "Orders",
                column: "OrderRequestId",
                principalTable: "OrderRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderRequests_OrderRequestId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderRequestId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderRequestId",
                table: "Orders");
        }
    }
}
