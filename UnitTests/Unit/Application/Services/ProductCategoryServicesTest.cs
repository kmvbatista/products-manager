using FluentAssertions;
using Application.Models.ProductCategoryModels;
using Application.Services;
using Application.Services.NotificationService;
using Application.Services.ReportService;
using Domain.Entity;
using Infra.Repositories.ProductCategory;
using Tests.Builders;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit.Application.Services
{
  public class ProductCategoryServicesTests
  {
    private readonly IProductCategoryService _productService;
    private readonly IProductCategoryRepository _productRepository;
    private readonly ISupplierService _supplierService;
    private readonly IExcelService<ProductCategoryResponseModel> _excelService;
    private readonly INotificationService _notificationService;

    public ProductCategoryServicesTests()
    {
      _productRepository = Substitute.For<IProductCategoryRepository>();
      _supplierService = Substitute.For<ISupplierService>();
      _excelService = Substitute.For<IExcelService<ProductCategoryResponseModel>>();
      _notificationService = Substitute.For<INotificationService>();
      _productService = new ProductCategoryService(_productRepository, _supplierService, _excelService, _notificationService);
    }

    [Fact]
    public void ShouldSaveProductCategory()
    {
      var supplierId = Guid.NewGuid();
      var model = new ProductCategoryRequestModel()
      {
        SupplierId = supplierId,
        CategoryName = "celular"
      };

      _supplierService.ExistsById(supplierId).Returns(true);

      _productService.Create(model);

      _productRepository.Received(1)
          .Create(Arg.Is<ProductCategory>(x => x.CategoryName.ToString() == "celular" && x.SupplierId == supplierId));
    }

    [Fact]
    public async Task ShouldGetProductPorId()
    {
      var supplierId = Guid.NewGuid();
      var productId = Guid.NewGuid();
      var user = new CategoryProductBuilder()
          .WithSupplier(supplierId)
          .WithName("celular")
          .Construct();

      _productRepository
          .GetById(productId)
          .Returns(user);

      var productRetornado = await _productService.GetById(productId);

      Assert.Equal("celular", productRetornado.CategoryName);
    }

    [Fact]
    public async Task ShouldUpdateProduct()
    {
      var productId = Guid.NewGuid();
      var supplierId = Guid.NewGuid();
      var model = new ProductCategoryRequestModel
      {
        SupplierId = Guid.NewGuid(),
        CategoryName = "celular"
      };

      var product = new CategoryProductBuilder()
          .WithSupplier(supplierId)
          .WithName("carro")
          .Construct();

      _productRepository
          .GetById(supplierId)
          .Returns(product);

      _productRepository.ValidateEntityExistence(productId).Returns(true);

      _productRepository.GetById(productId).Returns(product);

      await _productService.Update(productId, model);

      await _productRepository
          .Received(1)
          .Update(Arg.Is<ProductCategory>(x => x.CategoryName.ToString() == "celular"));
    }

    [Fact]
    public async Task should_dar_erro_quando_nao_encontrar_categoria_para_update()
    {
      var productId = Guid.Empty;
      var supplierId = Guid.NewGuid();
      var model = new ProductCategoryRequestModel
      {
        SupplierId = Guid.NewGuid(),
        CategoryName = "celular"
      };

      var exception = await Assert.ThrowsAsync<Exception>(async () => await _productService.Update(productId, model));

      exception.Message.Should().Be("Categoria de produto não encontrada");
      await _productRepository
          .DidNotReceive()
          .Update(Arg.Any<ProductCategory>());
    }

    [Fact]
    public async Task should_fail_when_category_name_invalid()
    {
      var supplierId = Guid.NewGuid();
      var model = new ProductCategoryRequestModel()
      {
        SupplierId = supplierId,
        CategoryName = ""
      };

      _supplierService.ExistsById(supplierId).Returns(true);

      await _productService.Create(model);

      Assert.True(_notificationService.HasNotifications());
    }
  }
}
