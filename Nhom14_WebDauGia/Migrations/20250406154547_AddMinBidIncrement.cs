using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddMinBidIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Min_Bid_Increment",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Min_Bid_Increment",
                table: "Products");
        }
    }
}
