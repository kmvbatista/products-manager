using FluentValidation;
using Domain.Entity;

namespace Domain.Validators
{
  public class UserValidator : AbstractValidator<User>
  {
    public UserValidator()
    {
      RuleFor(x => x.Name).SetValidator(new NameValidator());
      RuleFor(x => x.Password.Length).InclusiveBetween(8, 20).WithMessage("Senha deve ter entre 8 e 20 caracteres");
    }
  }
}
