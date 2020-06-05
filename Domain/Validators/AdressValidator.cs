using FluentValidation;
using Domain.ValueObjects;

namespace Domain.Validators
{
  public class AdressValidator : AbstractValidator<Address>
  {
    public AdressValidator()
    {
      RuleFor(end => end.Street.Length).ExclusiveBetween(1, 30);
      RuleFor(end => end.State.Length).ExclusiveBetween(1, 30);
      RuleFor(end => end.City.Length).ExclusiveBetween(1, 30);
      RuleFor(end => end.HouseNumber).LessThan(100000);
    }
  }
}
