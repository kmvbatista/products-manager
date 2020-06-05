using Domain.Entity;
using System;

namespace Tests.Builders
{
  public class CategoryProductBuilder
  {
    private string _categoryName;
    private Guid _supplierId;

    public ProductCategory Construct()
    {
      return new ProductCategory(_categoryName, _supplierId);
    }

    public CategoryProductBuilder WithName(string text)
    {
      _categoryName = text;
      return this;
    }

    public CategoryProductBuilder WithSupplier(Guid supplierId)
    {
      _supplierId = supplierId;
      return this;
    }
  }
}
