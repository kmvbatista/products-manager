using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
  public class UserMapping
      : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.Property(d => d.Name).HasColumnName(nameof(User.Name)).IsRequired();
      builder.Property(d => d.Login).HasColumnName(nameof(User.Login)).IsRequired();
      builder.Property(d => d.Password).HasColumnName(nameof(User.Password)).IsRequired();
      builder.Property(d => d.IsActive).HasColumnName(nameof(User.IsActive)).IsRequired();
      builder.Property(x => x.Name).HasConversion(y => y.ToString(), v => new Domain.ValueObjects.Name(v));
    }
  }
}
