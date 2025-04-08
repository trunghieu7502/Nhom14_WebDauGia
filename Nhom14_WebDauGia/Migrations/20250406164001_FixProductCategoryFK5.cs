using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class FixProductCategoryFK5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Products_Product_ID",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_Product_ID",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Product_ID",
                table: "Auctions");

            migrationBuilder.AlterColumn<decimal>(
                name: "Aut_Winner_Amount",
                table: "Auctions",
                type: "money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Aut_Reserve_Price",
                table: "Auctions",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_Auc_Item_ID",
                table: "Auctions",
                column: "Auc_Item_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Products_Auc_Item_ID",
                table: "Auctions",
                column: "Auc_Item_ID",
                principalTable: "Products",
                principalColumn: "Product_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Products_Auc_Item_ID",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_Auc_Item_ID",
                table: "Auctions");

            migrationBuilder.AlterColumn<decimal>(
                name: "Aut_Winner_Amount",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Aut_Reserve_Price",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<int>(
                name: "Product_ID",
                table: "Auctions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_Product_ID",
                table: "Auctions",
                column: "Product_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Products_Product_ID",
                table: "Auctions",
                column: "Product_ID",
                principalTable: "Products",
                principalColumn: "Product_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
