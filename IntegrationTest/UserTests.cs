using Application.Models;
using Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
  public class UserTests
  {
    private readonly HttpClient _client;

    public UserTests()
    {
      var appFactory = new WebApplicationFactory<Startup>();
      _client = appFactory.CreateClient();
    }

    [Fact]
    public async Task shoudFailWhen()
    {
      var user = new UserRequestModel
      {
        IsActive = true,
        Login = "kennedy123",
        Name = "kennedy",
        Password = "123456"
      };
      var serializedUser = JsonConvert.SerializeObject(user);
      var response = await _client.PostAsync("/api/user", new StringContent(serializedUser, Encoding.UTF8, "application/json"));
      var jsonResult = response.Content.ReadAsStringAsync().Result;
      var validationErrorMessage = JsonConvert.DeserializeObject<List<ValidationErrorResponse>>(jsonResult);
      Assert.True(validationErrorMessage[0]._Message == "Senha deve ter entre 8 e 20 caracteres");
    }
  }
}
