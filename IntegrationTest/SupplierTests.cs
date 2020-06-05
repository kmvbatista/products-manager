using Application.Models.SupplierModels;
using Domain.ValueObjects;
using Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

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

      var response = await _client.PostAsync("/api/supplier",
        new StringContent(serializedSupplier, Encoding.UTF8, "application/json"));
      var jsonResult = await response.Content.ReadAsStringAsync();
      var validationErrorMessage = JsonConvert.
        DeserializeObject<List<ValidationErrorResponse>>(jsonResult);
      Assert.True(validationErrorMessage[0]._Message == "Senha deve ter entre 8 e 20 caracteres");
    }
  }
}
