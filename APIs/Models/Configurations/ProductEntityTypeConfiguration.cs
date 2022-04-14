using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIs.Models.Configurations
{
  public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
  {
    public void Configure(EntityTypeBuilder<Product> builder)
    {
      builder
        .Property(p => p.IsDeleted)
        .HasDefaultValue(false);

      builder
        .HasQueryFilter(p => !p.IsDeleted);

      builder
        .Property(p => p.CreatedDate)
        .HasDefaultValueSql("getdate()");

      builder
        .Property(p => p.UpdatedDate)
        .HasDefaultValueSql("getdate()");
    }
  }
}