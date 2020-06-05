namespace Domain.ValueObjects
{
  public struct Email
  {
    private readonly string _text;
    public Email(string email)
    {
      _text = email;
    }

    public override string ToString()
    {
      return _text;
    }
  }
}
