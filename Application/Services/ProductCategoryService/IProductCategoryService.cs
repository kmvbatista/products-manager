using Application.Models.ProductCategoryModels;
using Domain.Entity;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services
{
  public interface IProductCategoryService : IServiceCrud<ProductCategoryRequestModel, ProductCategoryResponseModel>
  {
    Task<byte[]> GenerateExcelReport();
    Task<string> ReadExcelFile(Stream formFile);
    Task<IList<ProductCategory>> GetAllRaw();
  }
}
