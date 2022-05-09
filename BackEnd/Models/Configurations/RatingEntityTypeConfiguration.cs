using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Models.Configurations
{
  public class RatingEntityTypeConfiguration : IEntityTypeConfiguration<Rating>
  {
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
      builder
        .Property(p => p.IsDeleted)
        .HasDefaultValue(false);

      builder
        .HasQueryFilter(r => !r.IsDeleted);
    }
  }
}