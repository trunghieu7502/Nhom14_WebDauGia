using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Admin_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Admin_FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Admin_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Admin_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Admin_Phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Admin_ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Product_Cate_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Cate_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Product_Cate_ID);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Admin_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product_Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product_Start_Bid_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Product_Cate_ID = table.Column<int>(type: "int", nullable: false),
                    Seller_ID = table.Column<int>(type: "int", nullable: false),
                    ProductCategoryProduct_Cate_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product_ID);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryProduct_Cate_ID",
                        column: x => x.ProductCategoryProduct_Cate_ID,
                        principalTable: "ProductCategories",
                        principalColumn: "Product_Cate_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auctions",
                columns: table => new
                {
                    Auction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aut_Start_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aut_End_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aut_Reserve_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aut_Payment_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Aut_Winner_FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Aut_Winner_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Auc_Item_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auctions", x => x.Auction_ID);
                    table.ForeignKey(
                        name: "FK_Auctions_Products_Auc_Item_ID",
                        column: x => x.Auc_Item_ID,
                        principalTable: "Products",
                        principalColumn: "Product_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Shipment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shipment_Planned_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Shipment_Actual_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Shipment_Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Shipment_Item_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Shipment_ID);
                    table.ForeignKey(
                        name: "FK_Shipments_Products_Shipment_Item_ID",
                        column: x => x.Shipment_Item_ID,
                        principalTable: "Products",
                        principalColumn: "Product_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Pay_Method_Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pay_Method_Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Auction_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Pay_Method_Code);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_Auctions_Auction_ID",
                        column: x => x.Auction_ID,
                        principalTable: "Auctions",
                        principalColumn: "Auction_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_Auc_Item_ID",
                table: "Auctions",
                column: "Auc_Item_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_Auction_ID",
                table: "PaymentMethods",
                column: "Auction_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryProduct_Cate_ID",
                table: "Products",
                column: "ProductCategoryProduct_Cate_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_Shipment_Item_ID",
                table: "Shipments",
                column: "Shipment_Item_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "Auctions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductCategories");
        }
    }
}
