using Domain.Entity;
using Infra.Context;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IntegrationTests
{
  public class TestsFixture
  {
    //public const string FirstItemId = "312658D1-8146-42E3-B57B-360427182811";
    //public const string SecondItemId = "64C7E3F5-74F9-4540-9B12-BC7AFBCC7CE6";

    //public static readonly ToDoItem FirstItem = new ToDoItem() { Id = Guid.Parse(FirstItemId), Name = "Item 1" };
    //public static readonly ToDoItem SecondItem = new ToDoItem() { Id = Guid.Parse(SecondItemId), Name = "Item 2" };
    public readonly User user1 = new User("kennedy batista", "kennedy12", "123456789", true);
    public readonly User user2 = new User("kennedy Messias", "kennedyMess", "123456789", true);



    private readonly MainContext _context;

    public TestsFixture(MainContext context)
    {
      _context = context;

      _context.Database.EnsureDeleted();
      _context.Database.EnsureCreated();
    }

    public void SeedToDoItems()
    {
      _context.Set<User>().Add(user1);
      _context.Set<User>().Add(user2);
      _context.SaveChanges();
    }
  }

  public abstract class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
  where TStartup : class
  {
    protected override IWebHostBuilder CreateWebHostBuilder() =>
        WebHost.CreateDefaultBuilder().UseStartup<TStartup>();
  }

  public class WebApplicationFactoryWithInMemory : BaseWebApplicationFactory<TestStartup>
  {
    private readonly InMemoryDatabaseRoot _databaseRoot = new InMemoryDatabaseRoot();
    private readonly string _connectionString = Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.ConfigureServices(services =>
        {
          services.AddEntityFrameworkInMemoryDatabase()
                  .AddDbContext<MainContext>(options =>
                  {
                  options.UseInMemoryDatabase(_connectionString, _databaseRoot);
                  options.UseInternalServiceProvider(services.BuildServiceProvider());
                });
        });
  }

  public class WebApplicationFactoryWithInMemorySqlite : BaseWebApplicationFactory<TestStartup>
  {
    private readonly string _connectionString = "DataSource=:memory:";
    private readonly SqliteConnection _connection;

    public WebApplicationFactoryWithInMemorySqlite()
    {
      _connection = new SqliteConnection(_connectionString);
      _connection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.ConfigureServices(services =>
        {
          services
              .AddEntityFrameworkSqlite()
                  .AddEntityFrameworkSqlite()
                  .AddDbContext<MainContext>(options =>
                  {
                  options.UseSqlite(_connection);
                  options.UseInternalServiceProvider(services.BuildServiceProvider());
                });
        });

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      _connection.Close();
    }
  }

  public class WebApplicationFactoryWithSqlite : BaseWebApplicationFactory<TestStartup>
  {
    private readonly string _connectionString = $"DataSource={Guid.NewGuid()}.db";

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.ConfigureServices(services =>
        {
          services
                  .AddEntityFrameworkSqlite()
                  .AddDbContext<MainContext>(options =>
                  {
                  options.UseSqlite(_connectionString);
                  options.UseInternalServiceProvider(services.BuildServiceProvider());
                });
        });
  }
}
