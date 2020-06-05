using FluentValidation;
using Domain.ValueObjects;

namespace Domain.Validators
{
  public class NameValidator : AbstractValidator<Name>
  {
    public NameValidator()
    {
      RuleFor(name => name.ToString().Length).InclusiveBetween(3, 30).WithMessage("Nome inválido");
    }
  }
}