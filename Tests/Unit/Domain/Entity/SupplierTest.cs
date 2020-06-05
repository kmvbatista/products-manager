using FluentAssertions;
using Domain.ValueObjects;
using Tests.Builders;
using System;
using System.Threading.Tasks;
using Xunit;
namespace Tests.Unit.Domain.Entity
{
  public class SupplierTest
  {
    public SupplierTest()
    {
    }

    [Fact]
    public void should_update_supplier()
    {
      var supplierId = Guid.NewGuid();
      var supplier = new SupplierBuilder()
          .Withcpnj("85.224.941/0001-61")
          .WithEmail(new Email("joao@joazinho.com"))
          .WithId(supplierId)
          .WithCorporateName("Joao ltda")
          .WithTradingName("Bar do joao")
          .WithTelephone(new Telephone("(47)99620-7702"))
          .WithAdress(new Address("Rua amazonas", "bairro joaonese", "marilandia", "SC", 102))
          .Construct();

      supplier.Update("joaozinho ltda", "11.444.443/0001-98", "bar do joaozinho", "joao32@joaozinho.com",
          "(47)99620-7702", new Address("Rua amazonas", "bairro carlos silva", "cascalho", "SC", 107));

      supplier.Email.ToString().Should().Be("joao32@joaozinho.com");
      supplier.TradingName.Should().Be("bar do joaozinho");
      supplier.CorporateName.Should().Be("joaozinho ltda");
      supplier.Address.Should().Equals(new Address("Rua amazonas", "bairro carlos silva", "cascalho", "SC", 107));
      supplier.cpnj.Should().Be("11.444.443/0001-98");
    }
  }
}
