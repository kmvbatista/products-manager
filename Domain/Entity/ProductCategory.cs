using Domain.Validators;
using Domain.ValueObjects;
using FluentValidation;
using System;

namespace Domain.Entity
{
  public class ProductCategory : BaseEntity
  {
    public Name CategoryName { get; protected set; }
    public Guid SupplierId { get; protected set; }
    public virtual Supplier Supplier { get; set; }
    public bool IsActive { get; protected set; }

    public ProductCategory(string name, Guid supplier)
    {
      CategoryName = new Name(name);
      SupplierId = supplier;
      Validate(this, new ProductCategoryValidator());
    }

    public void Update(string name, Guid supplier)
    {
      CategoryName = new Name(name);
      SupplierId = supplier;
      Validate(this, new ProductCategoryValidator());
    }

    protected ProductCategory()
    {

    }


  }
}
