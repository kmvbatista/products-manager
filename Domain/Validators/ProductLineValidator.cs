using FluentValidation;
using Domain.Entity;

namespace Domain.Validators
{
  public class ProductLineValidator : AbstractValidator<ProductLine>
  {
    public ProductLineValidator()
    {
      RuleFor(x => x.Name).SetValidator(new NameValidator());
      RuleFor(x => x.ProductCategoryId).NotEqual(System.Guid.Empty).WithMessage("Id da categoria de produto está inválido");
    }
  }
}
