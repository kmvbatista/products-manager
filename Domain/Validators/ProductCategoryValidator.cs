using FluentValidation;
using Domain.Entity;

namespace Domain.Validators
{
  public class ProductCategoryValidator : AbstractValidator<ProductCategory>
  {
    public ProductCategoryValidator()
    {
      RuleFor(x => x.CategoryName).SetValidator(new NameValidator());
      RuleFor(x => x.Supplier).SetValidator(new SupplierValidator());
    }
  }
}
