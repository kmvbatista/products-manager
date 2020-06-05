using System;

namespace Application.Models.ProductCategoryModels
{
  public class ProductCategoryResponseModel : ProductCategoryBaseModel
  {
    public Guid Id { get; set; }
    public string SupplierName { get; set; }
    public Guid SupplierId { get; set; }
    public string IsActive { get; set; }
  }
}
