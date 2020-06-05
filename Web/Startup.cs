using DemoNotifications.Api.Filters;
using Application.Models.ProductCategoryModels;
using Application.Models.ProductLineModels;
using Application.Services;
using Application.Services.NotificationService;
using Application.Services.ProductLineService;
using Application.Services.ReportService;
using Application.Services.ReportServices;
using Domain.DomainNotifications;
using Domain.Interfaces;
using Infra.Context;
using Infra.Filter;
using Infra.Repositories.ProductCategory;
using Infra.Repositories.ProductLine;
using Infra.Repositories.Supplier;
using Infra.Repositories.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

      services.AddMvc(options =>
         options.Filters.Add(typeof(JsonExceptionFilter))
     );

      services.AddMvc(options => options.Filters.Add<NotificationFilter>());

      services.AddMvc();
      ConfigureDatabaseServices(services);
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IUserService, UserService>();
      services.AddTransient<ISupplierService, SupplierService>();
      services.AddTransient<ISupplierRepository, SupplierRepository>();
      services.AddTransient<IProductCategoryService, ProductCategoryService>();
      services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
      services.AddTransient<IProductLineService, ProductLineService>();
      services.AddTransient<IProductLineRepository, ProductLineRepository>();
      services.AddTransient<IExcelService<ProductCategoryResponseModel>, ExcelService<ProductCategoryResponseModel>>();
      services.AddTransient<IExcelService<ProductLineResponseModel>, ExcelService<ProductLineResponseModel>>();
      services.AddScoped<NotificationContext>();
      services.AddScoped<INotificationService, NotificationService>();
      services.AddCors();
    }

    public virtual void ConfigureDatabaseServices(IServiceCollection services)
    {
      services.AddDbContext<MainContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("ProdutosConnectionString")));
    }

    public virtual void Configure(IApplicationBuilder app, IHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }

      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetRequiredService<MainContext>();
        context.Database.Migrate();
      }

      app.UseHttpsRedirection();
      app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    }
  }
}
