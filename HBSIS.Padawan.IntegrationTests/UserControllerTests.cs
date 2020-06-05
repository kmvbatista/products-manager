using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
  public class UserControllerTests
  {
    protected BaseWebApplicationFactory<TestStartup> Factory { get; }

    protected UserControllerTests(BaseWebApplicationFactory<TestStartup> factory) =>
        Factory = factory;

    [Fact]
    public async Task GetEndpointsReturnSuccessAndCorrectContentType()
    {
      const string expectedContentType = "aplication/json; charset=utf-8";
      var client = Factory.CreateClient();

      var response = await client.GetAsync("/api/user");

      response.EnsureSuccessStatusCode();
      Assert.Equal(expectedContentType,
          response.Content.Headers.ContentType.ToString());
    }
  }
}
