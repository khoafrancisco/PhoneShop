using PhoneShop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PhoneShop.Models;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } 
    public DbSet<Customers> Customers  { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses  { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<Shipping> Shippings { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Media> Medias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}