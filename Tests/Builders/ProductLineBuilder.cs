using Domain.Entity;
using System;

namespace Tests.Builders
{
  public class ProductLineBuilder
  {
    public string Name { get; private set; }
    public Guid ProductCategoryId { get; private set; }
    public ProductCategory ProductCategory { get; private set; }
    public bool IsActive { get; private set; }

    public ProductLine Construct()
    {
      return new ProductLine(Name, ProductCategoryId, ProductCategory);
    }

    public ProductLineBuilder WithName(string name)
    {
      Name = name;
      return this;
    }

    public ProductLineBuilder WithProductCategoryId(Guid guid)
    {
      ProductCategoryId = guid;
      return this;
    }

    public ProductLineBuilder WhithCategory(ProductCategory productCategory)
    {
      ProductCategory = productCategory;
      return this;
    }
  }
}
