using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class Add_Min_Bid_Increment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Min_Bid_Increment",
                table: "Auctions",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Min_Bid_Increment",
                table: "Auctions");
        }
    }
}
