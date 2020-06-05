using Application.Models.ProductLineModels;
using Application.Services.NotificationService;
using Application.Services.ReportService;
using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductLineService
{
  public class ProductLineService : IProductLineService
  {
    private readonly IProductLineRepository _productLineRepository;
    private readonly IProductCategoryService _productCategoryService;
    private readonly IExcelService<ProductLineResponseModel> _excelService;
    private readonly INotificationService _notificationService;

    public ProductLineService(IProductLineRepository productLineRepository,
                            IProductCategoryService productCategoryService,
                            IExcelService<ProductLineResponseModel> excelService,
                            INotificationService notificationService)
    {
      _productLineRepository = productLineRepository;
      _productCategoryService = productCategoryService;
      _excelService = excelService;
      _notificationService = notificationService;
    }

    public async Task Create(ProductLineRequestModel request)
    {
      var newProductLine = new ProductLine(request.Name, request.ProductCategoryId);
      if (newProductLine.Invalid)
      {
        _notificationService.AddEntityNotifications(newProductLine.ValidationResult);
        return;
      }
      var productCategoryExists = await _productCategoryService.GetById(request.ProductCategoryId);
      if (productCategoryExists is null)
      {
        _notificationService.AddNotification("Erro ao salvar linha de produto", "Categoria de produto especificada não existe");
        return;
      }
      await _productLineRepository.Create(newProductLine);
    }

    public async Task Delete(Guid id)
    {
      if (id == Guid.Empty)
        _notificationService.AddNotification("erro ao deletar", "Id da linha de produto está inválido");
      await _productLineRepository.Delete(id);
    }

    public async Task<IList<ProductLineResponseModel>> GetAll()
    {
      var productsLines = await _productLineRepository.GetAll();
      if (productsLines != null)
      {
        return productsLines.Select(d => new ProductLineResponseModel
        {
          Name = d.Name.ToString(),
          ProductCategoryName = d.ProductCategory.CategoryName.ToString()
        }).ToList();
      }
      _notificationService.AddNotification("ProductLineGetAllError", "Não há linhas de produtos cadastradas na base");
      return null;
    }

    public async Task<ProductLineResponseModel> GetById(Guid id)
    {
      var productLineFound = await _productLineRepository.GetById(id);
      if (productLineFound != null)
      {
        var result = new ProductLineResponseModel
        {
          ProductCategoryName = productLineFound.ProductCategory?.CategoryName.ToString(),
          Name = productLineFound.Name.ToString()
        };
        return result;
      }
      _notificationService.AddNotification("ProductLineGetByIdError", $"Não foi encontrado nenhuma linha de produto com o id {id}");
      return null;
    }

    public async Task Update(Guid id, ProductLineRequestModel request)
    {
      await _productCategoryService.ValidateEntityExistence(request.ProductCategoryId);
      if (_notificationService.HasNotifications())
        return;
      var productLineToUpdate = await _productLineRepository.GetById(id);
      productLineToUpdate.Update(request.Name, request.ProductCategoryId, request.IsActive);
      if (productLineToUpdate.Valid)
        await _productLineRepository.Update(productLineToUpdate);
      else
        _notificationService.AddEntityNotifications(productLineToUpdate.ValidationResult);
    }

    public async Task<byte[]> GenerateExcelReport()
    {
      var allProductLines = await GetAll();
      if (allProductLines is null)
      {
        _notificationService.AddNotification("Erro ao gerar excel", "Não há linha de produtos na base");
        return null;
      }
      return _excelService.WriteExcelFile(allProductLines.ToList());
    }


    public async Task<string> ReadExcelFile(Stream formFile)
    {
      var productCategories = _excelService.ReadExcelFile(formFile);
      return await UpdateProductLinesDatabase(productCategories);
    }

    private async Task<string> UpdateProductLinesDatabase(List<ProductLineResponseModel> productLinesFromExcel)
    {
      var productLinesToAdd = new List<ProductLine>();
      var productLinesToUpdate = new List<ProductLine>();
      var allProductLines = await _productLineRepository.GetAll();
      var allProductCategories = await _productCategoryService.GetAllRaw();
      var errors = new StringBuilder();

      foreach (var currentProductLine in productLinesFromExcel)
      {
        var productCategory = allProductCategories.FirstOrDefault(x => x.CategoryName.ToString() == currentProductLine.ProductCategoryName);
        var existingProductLine = allProductCategories.FirstOrDefault(x => x.CategoryName.ToString() == currentProductLine.Name);

        if (productCategory is null)
          errors.AppendLine($"Não foi encontrada categoria de produto com o nome {currentProductLine.ProductCategoryName}");

        else if (existingProductLine is null)
          productLinesToAdd.Add(new ProductLine(currentProductLine.Name, productCategory.Id));

        else if (!productCategory.Equals(existingProductLine))
          productLinesToUpdate.Add(new ProductLine(currentProductLine.Name, productCategory.Id));
      }

      if (errors.Length > 0)
        return errors.ToString();

      return string.Empty;
    }

    public async Task ValidateEntityExistence(Guid entityId)
    {
      var productLineExists = await _productLineRepository.ValidateEntityExistence(entityId);
      if (!productLineExists)
        _notificationService.AddNotification("ValidateEntityError", "Esta linha de produto não existe");
    }
  }
}
