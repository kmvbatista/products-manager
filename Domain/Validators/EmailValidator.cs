using FluentValidation;
using Domain.ValueObjects;

namespace Domain.Validators
{
  public class EmailValidator : AbstractValidator<Email>
  {
    public EmailValidator()
    {
      RuleFor(email => email.ToString()).EmailAddress();
    }
  }
}