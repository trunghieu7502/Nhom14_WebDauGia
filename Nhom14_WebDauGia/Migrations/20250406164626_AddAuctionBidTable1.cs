using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddAuctionBidTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionBids",
                columns: table => new
                {
                    Bid_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Auction_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Bid_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bid_Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionBids", x => x.Bid_ID);
                    table.ForeignKey(
                        name: "FK_AuctionBids_Auctions_Auction_ID",
                        column: x => x.Auction_ID,
                        principalTable: "Auctions",
                        principalColumn: "Auction_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionBids_UserAccounts_User_ID",
                        column: x => x.User_ID,
                        principalTable: "UserAccounts",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionBids_Auction_ID",
                table: "AuctionBids",
                column: "Auction_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionBids_User_ID",
                table: "AuctionBids",
                column: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionBids");
        }
    }
}
