using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIs.Models.Configurations
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