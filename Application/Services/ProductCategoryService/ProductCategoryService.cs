using Application.Models.ProductCategoryModels;
using Application.Services.NotificationService;
using Application.Services.ReportService;
using Domain.DomainNotifications;
using Domain.Entity;
using Infra.Repositories.ProductCategory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
  public class ProductCategoryService : IProductCategoryService
  {
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly ISupplierService _supplierService;
    private readonly IExcelService<ProductCategoryResponseModel> _excelService;
    private readonly INotificationService _notificationService;

    public ProductCategoryService(IProductCategoryRepository productCategoryRepository, ISupplierService supplierService,
        IExcelService<ProductCategoryResponseModel> excelService, INotificationService notificationService)
    {
      _productCategoryRepository = productCategoryRepository;
      _supplierService = supplierService;
      _excelService = excelService;
      _notificationService = notificationService;
    }

    public async Task Create(ProductCategoryRequestModel request)
    {
      await _supplierService.ValidateEntityExistence(request.SupplierId);
      if (_notificationService.HasNotifications())
        return;
      var productCategory = new ProductCategory(request.CategoryName, request.SupplierId);
      if (productCategory.Invalid)
      {
        _notificationService.AddEntityNotifications(productCategory.ValidationResult);
        return;
      }
      await _productCategoryRepository.Create(productCategory);
    }

    public async Task Delete(Guid id)
    {
      await ValidateEntityExistence(id);
      if (_notificationService.HasNotifications())
        return;
      await _productCategoryRepository.Delete(id);
    }

    public async Task<IList<ProductCategoryResponseModel>> GetAll()
    {
      var productCategories = await _productCategoryRepository
          .GetAll();
      if (productCategories is null)
      {
        _notificationService.AddNotification("GetAllError", "Não há produtos cadastrados");
        return null;
      }
      return productCategories.Select(d => new ProductCategoryResponseModel
      {
        SupplierName = d.Supplier.TradingName,
        CategoryName = d.CategoryName.ToString(),
        IsActive = d.IsActive ? "Sim" : "Não"
      }).ToList();
    }
    public async Task<IList<ProductCategory>> GetAllRaw()
    {
      return await _productCategoryRepository.GetAll();
    }


    public async Task<ProductCategoryResponseModel> GetById(Guid id)
    {
      var foundProduct = await _productCategoryRepository.GetById(id);
      if (foundProduct == null)
      {
        _notificationService.AddNotification("Erro ao encontrar categoria de produto", "Categoria de produto não encontrada");
        return null;
      }
      return new ProductCategoryResponseModel
      {
        SupplierName = foundProduct.Supplier?.TradingName,
        SupplierId = foundProduct.SupplierId,
        Id = foundProduct.Id,
        CategoryName = foundProduct.CategoryName.ToString(),
        IsActive = foundProduct.IsActive ? "Sim" : "Não"
      };
    }

    public async Task Update(Guid id, ProductCategoryRequestModel request)
    {
      await _supplierService.ValidateEntityExistence(request.SupplierId);
      var productToUpdate = await _productCategoryRepository.GetById(id);
      if (productToUpdate == null)
      {
        _notificationService.AddNotification("Erro ao atualizar categoria de produto", "Categoria de produto não encontrada");
        return;
      }
      productToUpdate.Update(request.CategoryName, request.SupplierId);
      await _productCategoryRepository.Update(productToUpdate);
    }

    public async Task ValidateEntityExistence(Guid entityId)
    {
      var entityExists = await _productCategoryRepository.ValidateEntityExistence(entityId);
      if (!entityExists)
        _notificationService.AddNotification("Validação de entidade", "Categoria de produto não existe");
    }

    public async Task<byte[]> GenerateExcelReport()
    {
      var productCategories = await GetAll();
      if (productCategories is null)
      {
        _notificationService.AddNotification("Validação de entidade", "Categoria de produto não existe");
        return null;
      }
      return _excelService.WriteExcelFile(productCategories.ToList());
    }

    public async Task<string> ReadExcelFile(Stream formFile)
    {
      var productCategories = _excelService.ReadExcelFile(formFile);
      return await UpdateProductCategoriesDatabase(productCategories);
    }

    public async Task<string> UpdateProductCategoriesDatabase(List<ProductCategoryResponseModel> productCategories)
    {
      var productsToAdd = new List<ProductCategory>();
      var productsToUpdate = new List<ProductCategory>();
      var allProducts = await _productCategoryRepository.GetAll();
      var allSuppliers = await _supplierService.GetAll();
      var errors = new StringBuilder();

      foreach (var productCategory in productCategories)
      {
        var supplier = allSuppliers.FirstOrDefault(x => x.CorporateName == productCategory.SupplierName);

        var existentProductCategory = allProducts.FirstOrDefault(x => x.CategoryName.ToString() == productCategory.CategoryName
            && x.Supplier.CorporateName == productCategory.SupplierName);

        if (supplier is null)
        {
          errors.Append($"O supplier {productCategory.SupplierName}, da categoria {productCategory.CategoryName}, não existe");
        }

        else if (existentProductCategory is null)
          productsToAdd.Add(new ProductCategory(productCategory.CategoryName, supplier.Id));

        else if (!productCategory.Equals(existentProductCategory))
          productsToUpdate.Add(new ProductCategory(productCategory.CategoryName, existentProductCategory.SupplierId));

        if (errors.Length > 0)
          return errors.ToString();
      }

      if (productsToAdd.Count > 0)
        await _productCategoryRepository.AddCategoryRange(productsToAdd);

      if (productsToUpdate.Count > 0)
        await _productCategoryRepository.UpdateCategoryRange(productsToUpdate);

      return string.Empty;
    }
  }
}
