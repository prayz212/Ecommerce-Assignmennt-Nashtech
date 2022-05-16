using BackEnd.Models;
using BackEnd.Models.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackEnd
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Product> Products { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Admin> Admins { get; set; }

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