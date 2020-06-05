using FluentAssertions;
using Application.Models.ProductCategoryModels;
using Application.Models.ProductLineModels;
using Application.Services;
using Application.Services.NotificationService;
using Application.Services.ProductLineService;
using Application.Services.ReportService;
using Domain.Entity;
using Domain.Interfaces;
using Tests.Builders;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit.Application.Services
{
  public class ProductLineServiceTest
  {
    private readonly IProductLineService _productLineService;
    private readonly IProductLineRepository _productLineRepository;
    private readonly IProductCategoryService _productCategoryService;
    private readonly IExcelService<ProductLineResponseModel> _excelService;
    private readonly INotificationService _notificationContext;

    public ProductLineServiceTest()
    {
      _productLineRepository = Substitute.For<IProductLineRepository>();
      _productCategoryService = Substitute.For<IProductCategoryService>();
      _excelService = Substitute.For<IExcelService<ProductLineResponseModel>>();
      _notificationContext = Substitute.For<INotificationService>();
      _productLineService = new ProductLineService(_productLineRepository, _productCategoryService, _excelService, _notificationContext);
    }

    [Fact]
    public async Task ShouldCreateProductLine()
    {
      var ProductCategoryId = Guid.NewGuid();
      var ProductLineId = Guid.NewGuid();
      var model = new ProductLineRequestModel
      {
        Name = "Nokia 320",
        ProductCategoryId = ProductCategoryId
      };

      var ProductCategory = new ProductCategoryResponseModel
      {
        CategoryName = "celular"
      };
      _productCategoryService.GetById(ProductCategoryId).Returns(ProductCategory);

      await _productLineService.Create(model);

      await _productLineRepository.Received(1)
          .Create(Arg.Is<ProductLine>(x => x.Name.ToString() == model.Name && x.ProductCategoryId == ProductCategoryId));
    }

    [Fact]
    public async Task ShoudFailWhenProductCategoryIdIsInvalid_whenCreate()
    {
      var model = new ProductLineRequestModel
      {
        Name = "dfsdfdfasdfas"
      };

      var exception = await Assert.ThrowsAsync<Exception>(async () => await _productLineService.Create(model));
      Assert.Equal("Id da categoria de produto está inválido", exception.Message);
    }

    [Fact]
    public async Task ShouldDeleteProductLine()
    {
      var guid = Guid.NewGuid();
      await _productLineService.Delete(guid);
      await _productLineRepository.Received(1)
          .Delete(guid);
    }

    [Fact]
    public async Task ShoudGetById()
    {
      var productLineId = Guid.NewGuid();
      var productCategory = new CategoryProductBuilder()
          .WithName("celular")
          .Construct();

      var productLine = new ProductLineBuilder().
          WithProductCategoryId(productLineId)
          .WithName("nokia 780")
          .WhithCategory(productCategory)
          .Construct();

      _productLineRepository.GetById(productLineId).Returns(productLine);

      var productLineFound = await _productLineService.GetById(productLineId);

      Assert.Equal("nokia 780", productLineFound.Name);
    }

    [Fact]
    public async Task ShoudGetAll()
    {
      var productCategory = new CategoryProductBuilder()
          .WithName("celular")
          .Construct();


      var productLine = new ProductLineBuilder().
          WithProductCategoryId(Guid.NewGuid())
          .WithName("nokia 780")
          .WhithCategory(productCategory)
          .Construct();

      var productLine2 = new ProductLineBuilder().
          WithProductCategoryId(Guid.NewGuid())
          .WithName("nokia 7080")
          .WhithCategory(productCategory)
          .Construct();

      var productLineResponse = new ProductLineResponseModel
      {
        Name = "nokia 780",
        ProductCategoryName = productCategory.CategoryName.ToString()
      };

      var productsList = new List<ProductLine>();
      productsList.Add(productLine);
      productsList.Add(productLine2);

      _productLineRepository.GetAll().Returns(productsList);

      var result = await _productLineService.GetAll();

      result[0].Should().BeEquivalentTo(productLineResponse);
    }

    [Fact]
    public async Task ShoudFailWhenIdInvalid_whenCallingGetById()
    {
      ProductLine productLine = null;
      _productLineRepository.GetById(Guid.Empty).Returns(productLine);
      await Assert.ThrowsAsync<Exception>(async () => await _productLineService.GetById(Guid.Empty));
    }

    [Fact]
    public async Task should_update_product_line()
    {
      var ProductCategoryId = Guid.NewGuid();
      var ProductLineId = Guid.NewGuid();
      var model = new ProductLineRequestModel
      {
        Name = "Nokia 210",
        ProductCategoryId = ProductCategoryId
      };

      var productLineToUpdate = new ProductLineBuilder()
          .WithName("nokia A")
          .WithProductCategoryId(ProductCategoryId)
          .Construct();


      _productLineRepository.ValidateEntityExistence(ProductCategoryId).Returns(true);
      _productLineRepository.GetById(ProductLineId).Returns(productLineToUpdate);

      await _productLineService.Update(ProductLineId, model);

      await _productLineRepository
          .Received(1)
          .Update(Arg.Is<ProductLine>(x => x.ProductCategoryId == ProductCategoryId
                                                          && x.Name.ToString() == model.Name));
    }
  }
}

