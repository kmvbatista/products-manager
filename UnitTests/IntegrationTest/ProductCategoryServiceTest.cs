using Application.Models;
using Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.IntegrationTest
{
  public class ProductCategoryServiceTest : IClassFixture<WebApplicationFactory<Startup>>
  {
    private readonly WebApplicationFactory<Startup> _factory;
    public ProductCategoryServiceTest(WebApplicationFactory<Startup> factory)
    {
      _factory = factory;
    }

    [Fact]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
    {
      var client = _factory.CreateClient();
      var userRequestModel = new UserRequestModel()
      {
        Login = "kennedy123",
        Name = "Kennedy",
        IsActive = true,
        Password = "12345678910"
      };


      var jsonContent = JsonConvert.SerializeObject(userRequestModel);
      var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");
      contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

      var response = await client.PostAsync("/api/user", contentString);
      response.EnsureSuccessStatusCode();
    }
  }
}
