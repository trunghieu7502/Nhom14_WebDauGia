using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class FixProductCategoryFK4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Products_Auc_Item_ID",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_Auc_Item_ID",
                table: "Auctions");

            migrationBuilder.AlterColumn<string>(
                name: "Aut_Winner_FullName",
                table: "Auctions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Aut_Winner_FullName",
                table: "Auctions",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

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
    }
}
