using Domain.Entity;

namespace Domain.ValueObjects
{
  public class Address : BaseEntity
  {
    protected Address()
    {

    }

    public Address(string street, string neighborhood, string city, string state, int houseNumber)
    {
      Street = street;
      Neighborhood = neighborhood;
      City = city;
      State = state;
      HouseNumber = houseNumber;
    }

    public string Street { get; protected set; }
    public int HouseNumber { get; protected set; }
    public string Neighborhood { get; protected set; }
    public string City { get; protected set; }
    public string State { get; protected set; }
  }
}
