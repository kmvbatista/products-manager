using System;

namespace Application.Models.ProductCategoryModels
{
  public class ProductCategoryRequestModel : ProductCategoryBaseModel
  {
    public Guid SupplierId { get; set; }
  }
}
