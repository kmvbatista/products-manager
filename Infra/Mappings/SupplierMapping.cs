using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
  public class SupplierMapping
      : IEntityTypeConfiguration<Supplier>
  {
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
      builder.Property(d => d.TradingName).IsRequired();
      builder.Property(d => d.CorporateName).IsRequired();
      builder.Property(d => d.cpnj).IsRequired();
      builder.Property(d => d.Email).HasConversion(v => v.ToString(), v => new Domain.ValueObjects.Email(v));
      builder.Property(d => d.Telephone).HasConversion(v => v.ToString(), v => new Domain.ValueObjects.Telephone(v));
    }
  }
}
