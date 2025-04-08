﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuctionWeb.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    [Migration("20250406164626_AddAuctionBidTable1")]
    partial class AddAuctionBidTable1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuctionWeb.Models.Admin", b =>
                {
                    b.Property<int>("Admin_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Admin_ID"));

                    b.Property<string>("Admin_Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Admin_Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Admin_FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Admin_Phone")
                        .HasColumnType("int");

                    b.HasKey("Admin_ID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("AuctionWeb.Models.Auction", b =>
                {
                    b.Property<int>("Auction_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Auction_ID"));

                    b.Property<int>("Auc_Item_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Aut_End_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Aut_Payment_Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Aut_Reserve_Price")
                        .HasColumnType("money");

                    b.Property<DateTime>("Aut_Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Aut_Winner_Amount")
                        .HasColumnType("money");

                    b.Property<string>("Aut_Winner_FullName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Auction_ID");

                    b.HasIndex("Auc_Item_ID");

                    b.ToTable("Auctions");
                });

            modelBuilder.Entity("AuctionWeb.Models.AuctionBid", b =>
                {
                    b.Property<int>("Bid_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Bid_ID"));

                    b.Property<int>("Auction_ID")
                        .HasColumnType("int");

                    b.Property<decimal>("Bid_Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Bid_Time")
                        .HasColumnType("datetime2");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("Bid_ID");

                    b.HasIndex("Auction_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("AuctionBids");
                });

            modelBuilder.Entity("AuctionWeb.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Pay_Method_Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pay_Method_Code"));

                    b.Property<int>("Auction_ID")
                        .HasColumnType("int");

                    b.Property<string>("Pay_Method_Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Pay_Method_Code");

                    b.HasIndex("Auction_ID");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("AuctionWeb.Models.Product", b =>
                {
                    b.Property<int>("Product_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_ID"));

                    b.Property<decimal?>("Min_Bid_Increment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Product_Cate_ID")
                        .HasColumnType("int");

                    b.Property<string>("Product_Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Product_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Product_Start_Bid_Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Seller_ID")
                        .HasColumnType("int");

                    b.HasKey("Product_ID");

                    b.HasIndex("Product_Cate_ID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AuctionWeb.Models.ProductCategory", b =>
                {
                    b.Property<int>("Product_Cate_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_Cate_ID"));

                    b.Property<string>("Product_Cate_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Product_Cate_ID");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("AuctionWeb.Models.Shipment", b =>
                {
                    b.Property<int>("Shipment_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Shipment_ID"));

                    b.Property<DateTime>("Shipment_Actual_Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Shipment_Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Shipment_Item_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Shipment_Planned_Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Shipment_ID");

                    b.HasIndex("Shipment_Item_ID");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("AuctionWeb.Models.UserAccount", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_ID"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_Admin_ID")
                        .HasColumnType("int");

                    b.Property<string>("User_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("User_ID");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("AuctionWeb.Models.Auction", b =>
                {
                    b.HasOne("AuctionWeb.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Auc_Item_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AuctionWeb.Models.AuctionBid", b =>
                {
                    b.HasOne("AuctionWeb.Models.Auction", "Auction")
                        .WithMany()
                        .HasForeignKey("Auction_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuctionWeb.Models.UserAccount", "UserAccount")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("AuctionWeb.Models.PaymentMethod", b =>
                {
                    b.HasOne("AuctionWeb.Models.Auction", "Auction")
                        .WithMany()
                        .HasForeignKey("Auction_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");
                });

            modelBuilder.Entity("AuctionWeb.Models.Product", b =>
                {
                    b.HasOne("AuctionWeb.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("Product_Cate_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("AuctionWeb.Models.Shipment", b =>
                {
                    b.HasOne("AuctionWeb.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Shipment_Item_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
