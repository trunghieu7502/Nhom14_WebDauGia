using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddAuctionBidTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Auctions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Payment_Deadline",
                table: "Auctions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Payment_Deadline",
                table: "Auctions");
        }
    }
}
