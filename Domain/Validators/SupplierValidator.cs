using FluentValidation;
using Domain.Entity;

namespace Domain.Validators
{
  public class SupplierValidator : AbstractValidator<Supplier>
  {
    public SupplierValidator()
    {
      RuleFor(supplier => supplier.cpnj).Must(Validatecpnj).WithMessage("cpnj está errado");
      RuleFor(supplier => supplier.TradingName.Length).ExclusiveBetween(3, 30).WithMessage("Nome fantasia deve estar entre 3 e 30 caracterees");
      RuleFor(supplier => supplier.CorporateName.Length).ExclusiveBetween(3, 30).WithMessage("Razão social deve estar entre 3 e 30 caracterees");
      RuleFor(x => x.Email).SetValidator(new EmailValidator());
      RuleFor(supplier => supplier.Telephone).SetValidator(new TelephoneValidator()).WithMessage("telephone está errado");
      RuleFor(supplier => supplier.Address).SetValidator(new AdressValidator()).WithMessage("endereço está errado");
    }

    public bool Validatecpnj(string cpnj)
    {
      int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
      int sum;
      int remainder;
      string digit;
      string tempcpnj;
      cpnj = cpnj.Trim();
      cpnj = cpnj.Replace(".", "").Replace("-", "").Replace("/", "");
      if (cpnj.Length != 14)
        return false;
      tempcpnj = cpnj.Substring(0, 12);
      sum = 0;
      for (int i = 0; i < 12; i++)
        sum += int.Parse(tempcpnj[i].ToString()) * multiplier1[i];
      remainder = (sum % 11);
      if (remainder < 2)
        remainder = 0;
      else
        remainder = 11 - remainder;
      digit = remainder.ToString();
      tempcpnj = tempcpnj + digit;
      sum = 0;
      for (int i = 0; i < 13; i++)
        sum += int.Parse(tempcpnj[i].ToString()) * multiplier2[i];
      remainder = (sum % 11);
      if (remainder < 2)
        remainder = 0;
      else
        remainder = 11 - remainder;
      digit = digit + remainder.ToString();
      return cpnj.EndsWith(digit);
    }
  }
}
