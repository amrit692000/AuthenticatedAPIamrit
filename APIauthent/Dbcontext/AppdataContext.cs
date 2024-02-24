using Microsoft.EntityFrameworkCore;

namespace APIauthent.Dbcontext;

public class AppdataContext : DbContext
{ 
    public AppdataContext(DbContextOptions<AppdataContext> options)
    : base (options)
{}
  public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
}

