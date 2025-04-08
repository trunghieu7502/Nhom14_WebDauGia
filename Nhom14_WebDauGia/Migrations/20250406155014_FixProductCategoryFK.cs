using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class FixProductCategoryFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryProduct_Cate_ID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryProduct_Cate_ID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCategoryProduct_Cate_ID",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Product_Cate_ID",
                table: "Products",
                column: "Product_Cate_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_Product_Cate_ID",
                table: "Products",
                column: "Product_Cate_ID",
                principalTable: "ProductCategories",
                principalColumn: "Product_Cate_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_Product_Cate_ID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Product_Cate_ID",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductCategoryProduct_Cate_ID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryProduct_Cate_ID",
                table: "Products",
                column: "ProductCategoryProduct_Cate_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryProduct_Cate_ID",
                table: "Products",
                column: "ProductCategoryProduct_Cate_ID",
                principalTable: "ProductCategories",
                principalColumn: "Product_Cate_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
