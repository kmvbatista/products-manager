using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
  public class ProductCategoryMapping
      : IEntityTypeConfiguration<ProductCategory>
  {
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
      builder.HasOne(d => d.Supplier).WithMany().HasForeignKey(d => d.SupplierId).IsRequired();
      builder.Property(x => x.CategoryName).HasConversion(y => y.ToString(), v => new Domain.ValueObjects.Name(v));
    }
  }
}
