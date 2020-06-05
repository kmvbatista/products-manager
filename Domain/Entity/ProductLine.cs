using Domain.Validators;
using Domain.ValueObjects;
using System;

namespace Domain.Entity
{
  public class ProductLine : BaseEntity
  {
    public Name Name { get; private set; }
    public Guid ProductCategoryId { get; private set; }
    public ProductCategory ProductCategory { get; private set; }
    public bool IsActive { get; private set; }

    protected ProductLine()
    {

    }

    public ProductLine(string name, Guid CategoryId)
    {
      Name = new Name(name);
      ProductCategoryId = CategoryId;
      IsActive = true;
      Validate(this, new ProductLineValidator());
    }

    public ProductLine(string name, Guid CategoryId, ProductCategory productCategory)
    {
      Name = new Name(name);
      ProductCategoryId = CategoryId;
      IsActive = true;
      ProductCategory = productCategory;
      Validate(this, new ProductLineValidator());
    }

    public void Deactivate()
    {
      IsActive = false;
    }

    public void Update(string name, Guid ProductCategoryId, bool isActive)
    {
      Name = new Name(name);
      IsActive = isActive;
      Validate(this, new ProductLineValidator());
    }
  }
}
