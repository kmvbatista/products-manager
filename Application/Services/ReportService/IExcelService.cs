using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services.ReportService
{
  public interface IExcelService<ResponseModel>
  {
    byte[] WriteExcelFile(IEnumerable<ResponseModel> produtos);
    List<ResponseModel> ReadExcelFile(Stream formFileStream);
  }
}
