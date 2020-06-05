using Application.Models.ProductLineModels;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services.ProductLineService
{
  public interface IProductLineService : IServiceCrud<ProductLineRequestModel, ProductLineResponseModel>
  {
    Task<byte[]> GenerateExcelReport();
    Task<string> ReadExcelFile(Stream formFile);
  }
}
