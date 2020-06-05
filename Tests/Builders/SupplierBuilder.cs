using Domain.Entity;
using Domain.ValueObjects;
using System;

namespace Tests.Builders
{
  public class SupplierBuilder
  {
    private Guid _id;
    private string _tradingName;
    private string _corporateName;
    private string _cpnj;
    private Address _adress;
    private Email _email;
    private Telephone _telephone;

    public Supplier Construct()
    {
      return new Supplier(_corporateName, _cpnj, _tradingName, _email.ToString(), _telephone.ToString(), _adress)
      {
        Id = _id
      };
    }

    public SupplierBuilder WithCorporateName(string corporateName)
    {
      _corporateName = corporateName;
      return this;
    }

    public SupplierBuilder WithTradingName(string tradingName)
    {
      _tradingName = tradingName;
      return this;
    }

    public SupplierBuilder WithAdress(Address address)
    {
      _adress = address;
      return this;
    }

    public SupplierBuilder WithTelephone(Telephone telephone)
    {
      _telephone = telephone;
      return this;
    }

    public SupplierBuilder WithEmail(Email email)
    {
      _email = email;
      return this;
    }

    public SupplierBuilder Withcpnj(string cpnj)
    {
      _cpnj = cpnj;
      return this;
    }

    public SupplierBuilder WithId(Guid id)
    {
      _id = id;
      return this;
    }
  }
}
