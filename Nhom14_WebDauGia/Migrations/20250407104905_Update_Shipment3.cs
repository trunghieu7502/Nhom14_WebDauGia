using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class Update_Shipment3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Auctions_Shipment_Auction_ID",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "Shipment_Auction_ID",
                table: "Shipments",
                newName: "Auction_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Shipments_Shipment_Auction_ID",
                table: "Shipments",
                newName: "IX_Shipments_Auction_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Auctions_Auction_ID",
                table: "Shipments",
                column: "Auction_ID",
                principalTable: "Auctions",
                principalColumn: "Auction_ID",
                onDelete: ReferentialAction.NoAction); // Specify ON DELETE NO ACTION
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Auctions_Auction_ID",
                table: "Shipments");

            migrationBuilder.RenameColumn(
                name: "Auction_ID",
                table: "Shipments",
                newName: "Shipment_Auction_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Shipments_Auction_ID",
                table: "Shipments",
                newName: "IX_Shipments_Shipment_Auction_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Auctions_Shipment_Auction_ID",
                table: "Shipments",
                column: "Shipment_Auction_ID",
                principalTable: "Auctions",
                principalColumn: "Auction_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
