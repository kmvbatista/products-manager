using Application.Models.SupplierModels;
using Domain.ValueObjects;
using Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{

  public class SupplierTests
  {
    private readonly HttpClient _client;

    public SupplierTests()
    {
      var appFactory = new WebApplicationFactory<Startup>();
      _client = appFactory.CreateClient();
    }

    [Theory]
    [InlineData("85.224.941/0001-61", "Joao ltda", "Bar do joao", "(47)99620-7702", "joao@joazinho.com")]
    public async Task ShouldFailWhenCnpjInvalid(string cpnj, string corporateName, string tradingName,
                                    string telephone, string email)
    {
      var adressModel = new Address("Rua amazonas", "bairro joaonese", "marilandia", "SAO PAULO", 102);
      var supplier = new SupplierRequestModel
      {
        Telephone = telephone,
        Email = email,
        Address = adressModel,
        CorporateName = corporateName,
        cpnj = cpnj,
        TradingName = tradingName
      };
      var serializedSupplier = JsonConvert.SerializeObject(supplier);

      var response = await _client.PostAsync("/api/supplier", new StringContent(serializedSupplier, Encoding.UTF8, "application/json"));
      //var response = await _client.GetAsync("api/supplier");

    }

    //[Fact]
    //public async Task Test1()
    //{
    //    var adressModel = new Address("Rua amazonas", "bairro joaonese", "marilandia", "SC", 102);

    //    var supplier = new SupplierBuilder()
    //        .WithAdress(null)
    //        .WithCorporateName("raua fjakdja�aaa")
    //        .Withcpnj("88.202.679/0001-42")
    //        .WithEmail(new Domain.ValueObjects.Email("carlos@gmail.com"))
    //        .WithTelephone(new Domain.ValueObjects.Telephone("(47) 99620-7702"))
    //        .WithTradingName("");

    //    var response = await _client.PostAsync("/api/supplier", supplier);
    //    Assert.Equal
    //}
  }
}
