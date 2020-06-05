using FluentValidation;
using Domain.Validators;
using Domain.ValueObjects;
using System;

namespace Domain.Entity
{
  public class Supplier : BaseEntity
  {
    public string CorporateName { get; set; }
    public string cpnj { get; set; }
    public string TradingName { get; set; }
    public virtual Address Address { get; set; }
    public virtual Email Email { get; set; }
    public virtual Telephone Telephone { get; set; }

    public Supplier(string corporateName, string Cpnj, string tradingName, string email, string telephone, Address _adress)
    {
      CorporateName = corporateName;
      cpnj = Cpnj;
      TradingName = tradingName;
      Email = new Email(email);
      Telephone = new Telephone(telephone);
      Address = _adress;
      Validate(this, new SupplierValidator());
    }

    public void Update(string corporateName, string Cpnj, string tradingName, string email, string telephone, Address _adress)
    {
      CorporateName = corporateName;
      cpnj = Cpnj;
      TradingName = tradingName;
      Email = new Email(email);
      Telephone = new Telephone(telephone);
      Address = _adress;
      Validate(this, new SupplierValidator());
    }

    protected Supplier()
    {
    }
  }
}
