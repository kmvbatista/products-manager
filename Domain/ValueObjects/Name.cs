namespace Domain.ValueObjects
{
  public struct Name
  {
    private readonly string _text;
    public Name(string name)
    {
      _text = name;
    }

    public override string ToString()
    {
      return _text;
    }
  }
}
