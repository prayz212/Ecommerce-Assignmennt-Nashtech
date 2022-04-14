using APIs.Models;
using APIs.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace APIs
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Product> Products { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      
      new CategoryEntityTypeConfiguration().Configure(builder.Entity<Category>());
      new ImageEntityTypeConfiguration().Configure(builder.Entity<Image>());
      new RatingEntityTypeConfiguration().Configure(builder.Entity<Rating>());
      new ProductEntityTypeConfiguration().Configure(builder.Entity<Product>());
    }
  }
}