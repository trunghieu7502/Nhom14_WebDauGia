using AuctionWeb.Models;
using Microsoft.EntityFrameworkCore;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<AuctionBid> AuctionBids { get; set; }
}
