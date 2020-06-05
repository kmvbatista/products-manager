namespace Domain.ValueObjects
{
  public struct Telephone
  {
    private readonly string _text;

    public Telephone(string telNumber)
    {
      _text = telNumber;
    }

    public override string ToString()
    {
      return _text;
    }
  }
}
