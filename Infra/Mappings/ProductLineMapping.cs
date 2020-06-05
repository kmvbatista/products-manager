using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
  public class ProductLineMapping
      : IEntityTypeConfiguration<ProductLine>
  {
    public void Configure(EntityTypeBuilder<ProductLine> builder)
    {
      builder.HasOne(d => d.ProductCategory).WithMany().HasForeignKey(d => d.ProductCategoryId).IsRequired();
      builder.Property(x => x.Name).HasConversion(y => y.ToString(), v => new Domain.ValueObjects.Name(v));
    }
  }
}
