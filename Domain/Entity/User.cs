using Domain.Validators;
using Domain.ValueObjects;

namespace Domain.Entity
{
  public class User
      : BaseEntity
  {
    public User(string name, string login, string password, bool isActive)
    {
      Name = new Name(name);
      Login = login;
      Password = password;
      IsActive = isActive;
      Validate(this, new UserValidator());
    }

    protected User()
    {

    }

    public void Update(string name, string login, string password, bool isActive)
    {
      Name = new Name(name);
      Login = login;
      Password = password;
      IsActive = isActive;
      Validate(this, new UserValidator());
    }

    public void Deactivate()
    {
      IsActive = false;
    }

    public Name Name { get; protected set; }
    public string Login { get; protected set; }
    public string Password { get; protected set; }
    public bool IsActive { get; protected set; }
  }
}
