using CsvHelper;
using Application.Services.ReportService;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Application.Services.ReportServices
{
  public class ExcelService<ResponseModel> : IExcelService<ResponseModel>
  {
    public byte[] WriteExcelFile(IEnumerable<ResponseModel> entityList)
    {
      byte[] file;
      using (var stream = new MemoryStream())
      using (var writer = new StreamWriter(stream))
      using (var csv = new CsvWriter(writer))
      {
        csv.Configuration.Delimiter = ";";
        csv.WriteRecords(entityList);
        writer.Flush();
        file = stream.ToArray();
      }
      return file;
    }

    public List<ResponseModel> ReadExcelFile(Stream formFileStream)
    {
      List<ResponseModel> records;
      using (var reader = new StreamReader(formFileStream))
      using (var csv = new CsvReader(reader))
      {
        records = csv.GetRecords<ResponseModel>().ToList();
      }
      return records;
    }
  }
}