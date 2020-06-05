
using Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests
{
  public class TestStartup : Startup
  {
    public TestStartup(IConfiguration configuration) : base(configuration)
    {
    }

    public override void ConfigureDatabaseServices(IServiceCollection services)
    {
      // Database providers are injected in WebApplicationFactoryWithPROVIDER.cs classes
      services.AddTransient<TestsFixture>();
    }

    public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      base.Configure(app, env);
      using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
      var seeder = serviceScope.ServiceProvider.GetService<TestsFixture>();
      seeder.SeedToDoItems();
    }
  }
}
