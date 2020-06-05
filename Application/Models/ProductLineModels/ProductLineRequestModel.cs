using System;

namespace Application.Models.ProductLineModels
{
  public class ProductLineRequestModel : ProductLineBaseModel
  {
    public Guid ProductCategoryId { get; set; }
    public bool IsActive { get; set; }
  }
}