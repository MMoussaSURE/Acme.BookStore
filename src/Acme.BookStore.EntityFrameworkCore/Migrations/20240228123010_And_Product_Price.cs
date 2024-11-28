using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class And_Product_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "AppProducts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_ClientId",
                table: "AppOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrderLines_ProductId",
                table: "AppOrderLines",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrderLines_AppProducts_ProductId",
                table: "AppOrderLines",
                column: "ProductId",
                principalTable: "AppProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppClients_ClientId",
                table: "AppOrders",
                column: "ClientId",
                principalTable: "AppClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrderLines_AppProducts_ProductId",
                table: "AppOrderLines");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppClients_ClientId",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_ClientId",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrderLines_ProductId",
                table: "AppOrderLines");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "AppProducts");
        }
    }
}
