using Domain.ValueObjects;

namespace Application.Models.SupplierModels
{
  public class SupplierModelBase
  {
    public string CorporateName { get; set; }
    public string cpnj { get; set; }
    public string TradingName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public Address Address { get; set; }
  }
}
