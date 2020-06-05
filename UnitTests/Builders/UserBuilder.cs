using Domain.Entity;
using System;

namespace Tests.Builders
{
  public class UserBuilder
  {
    private Guid _id;
    private string _name;
    private string _login;
    private string _password;
    private bool _isActive;

    public User Construct()
    {
      return new User(_name, _login, _password, _isActive)
      {
        Id = _id
      };
    }
    public UserBuilder WithName(string name)
    {
      _name = name;
      return this;
    }

    public UserBuilder ComLogin(string login)
    {
      _login = login;
      return this;
    }

    public UserBuilder ComSenha(string password)
    {
      _password = password;
      return this;
    }

    public UserBuilder IsActive()
    {
      _isActive = true;
      return this;
    }
    public UserBuilder InisActive()
    {
      _isActive = false;
      return this;
    }

    public UserBuilder WithId(Guid id)
    {
      _id = id;
      return this;
    }
  }
}
