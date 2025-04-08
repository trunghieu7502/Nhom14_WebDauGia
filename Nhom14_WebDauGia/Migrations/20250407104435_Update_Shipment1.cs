using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class Update_Shipment1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Shipment_Auction_ID",
                table: "Shipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_Shipment_Auction_ID",
                table: "Shipments",
                column: "Shipment_Auction_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Auctions_Shipment_Auction_ID",
                table: "Shipments",
                column: "Shipment_Auction_ID",
                principalTable: "Auctions",
                principalColumn: "Auction_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Auctions_Shipment_Auction_ID",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_Shipment_Auction_ID",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Shipment_Auction_ID",
                table: "Shipments");
        }
    }
}
