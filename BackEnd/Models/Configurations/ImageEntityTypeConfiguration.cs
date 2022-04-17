using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Models.Configurations
{
  public class ImageEntityTypeConfiguration : IEntityTypeConfiguration<Image>
  {
    public void Configure(EntityTypeBuilder<Image> builder)
    {
      builder
        .Property(i => i.IsDeleted)
        .HasDefaultValue(false);

      builder
        .HasQueryFilter(i => !i.IsDeleted);
    }
  }
}