using FluentAssertions;
using Application.Models.SupplierModels;
using Application.Services;
using Application.Services.NotificationService;
using Domain.Entity;
using Domain.ValueObjects;
using Infra.Repositories.Supplier;
using Tests.Builders;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit.Application.Services
{
  public class SupplierServiceTest
  {
    private readonly ISupplierService _supplierService;
    private readonly ISupplierRepository _supplierRepository;
    private INotificationService _notificationService;


    public SupplierServiceTest()
    {
      _supplierRepository = Substitute.For<ISupplierRepository>();
      _notificationService = Substitute.For<INotificationService>();
      _supplierService = new SupplierService(_supplierRepository, _notificationService);
    }

    [Theory]
    [InlineData("85.224.941/0001-61", "Joao ltda", "Bar do joao", "(47)99620-7702", "joao@joazinho.com")]
    public async Task ShouldCreateSupplier(string cpnj, string corporateName, string tradingName,
                                    string telephone, string email)
    {
      var adressModel = new Address("Rua amazonas", "bairro joaonese", "marilandia", "SC", 102);
      var userId = Guid.NewGuid();
      var model = new SupplierRequestModel()
      {
        cpnj = cpnj,
        CorporateName = corporateName,
        TradingName = tradingName,
        Telephone = telephone,
        Address = adressModel,
        Email = email
      };

      await _supplierService.Create(model);

      await _supplierRepository
          .Received(1)
          .Create(Arg.Is<Supplier>(x => x.TradingName == tradingName
          && x.CorporateName == corporateName
          && x.Telephone.ToString() == telephone
          && x.Email.ToString() == email
          && x.cpnj == cpnj
          && x.Address == adressModel
         ));
    }

    [Fact]
    public async Task ShouldUpdateSupplier()
    {
      var supplierId = Guid.NewGuid();
      var model = new SupplierRequestModel()
      {
        cpnj = "85.224.941/0001-61",
        TradingName = "Great bar",
        Email = "joao@joazinho.com",
        Address = new Address("Rua amazonas", "bairro joaonese", "marilandia", "SC", 102),
        CorporateName = "Joazinho carlos ltda",
        Telephone = "(47)99620-7702"
      };
      var supplier = new SupplierBuilder()
          .Withcpnj("85.224.941/0001-61")
          .WithEmail(new Email("joao@joazinho.com"))
          .WithAdress(new Address("Rua amazonas", "bairro joaonese", "marilandia", "SC", 102))
          .WithTradingName("Joao joazito")
          .WithCorporateName("Joazinho carlos ltda")
          .WithTelephone(new Telephone("(47)99620-7702"))
          .Construct();

      _supplierRepository
          .GetById(supplierId)
          .Returns(supplier);

      await _supplierService
          .Update(supplierId, model);

      await _supplierRepository
          .Received(1)
          .Update(Arg.Is<Supplier>(x => x.TradingName == "Great bar"));
    }

    [Fact]
    public async Task ShouldGetSupplierPorId()
    {
      var userId = Guid.NewGuid();
      var user = new SupplierBuilder()
          .Withcpnj("85.224.941/0001-61")
          .WithEmail(new Email("joao@joazinho.com"))
          .WithAdress(new Address("Rua amazonas", "bairro joaonese", "marilandia", "SC", 102))
          .WithTradingName("Great bar")
          .WithCorporateName("Joazinho carlos ltda")
          .WithTelephone(new Telephone("(47)99620-7702"))
          .Construct();

      _supplierRepository
          .GetById(userId)
          .Returns(user);

      var supplierRetornado = await _supplierService
          .GetById(userId);

      supplierRetornado.TradingName.Should().Be("Great bar");
    }

    [Fact]
    public async Task ShouldRetornarTodosSupplieres()
    {
      var supplierA = new SupplierBuilder()
          .Withcpnj("85.224.941/0001-61")
          .WithEmail(new Email("joao@joazinho.com"))
          .WithAdress(new Address("Rua amazonas", "bairro joaonese", "marilandia", "SC", 102))
          .WithTradingName("Great bar")
          .WithCorporateName("Joaozeti")
          .WithTelephone(new Telephone("(47)99620-7702"))
          .Construct();

      var supplierB = new SupplierBuilder()
          .Withcpnj("85.224.941/0001-61")
          .WithEmail(new Email("joao@joazo.com"))
          .WithAdress(new Address("Rua amazonas", "bairro joase", "joanópolis", "SC", 102))
          .WithTradingName("Great abar")
          .WithCorporateName("Joazaainho carlos ltda")
          .WithTelephone(new Telephone("(47)99720-7702"))
          .Construct();

      var supplieres = new List<Supplier>();
      supplieres.Add(supplierA);
      supplieres.Add(supplierB);

      _supplierRepository.GetAll().Returns(supplieres);

      var supplieresRetornados = await _supplierService.GetAll();

      supplieresRetornados.Should().HaveCount(2);
      supplieresRetornados[0].TradingName.Should().Be(supplierA.TradingName);
      supplieresRetornados[0].cpnj.Should().Be(supplierA.cpnj);
      supplieresRetornados[1].TradingName.Should().Be(supplierB.TradingName);
    }
  }
}
