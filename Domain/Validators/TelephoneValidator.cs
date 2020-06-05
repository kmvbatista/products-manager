using FluentValidation;
using Domain.ValueObjects;

namespace Domain.Validators
{
  public class TelephoneValidator : AbstractValidator<Telephone>
  {
    public TelephoneValidator()
    {
      RuleFor(telephone => telephone.ToString().Length).InclusiveBetween(10, 14);
    }
  }
}
