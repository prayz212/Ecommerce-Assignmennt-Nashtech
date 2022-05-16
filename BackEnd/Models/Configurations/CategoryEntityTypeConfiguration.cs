using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Models.Configurations
{
  public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
  {
    public void Configure(EntityTypeBuilder<Category> builder)
    {
      builder
        .Property(c => c.IsDeleted)
        .HasDefaultValue(false);

      builder
        .HasQueryFilter(c => !c.IsDeleted);
    }
  }
}