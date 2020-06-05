using Application.Models.ProductCategoryModels;
using Application.Models.ProductLineModels;
using Application.Services;
using Application.Services.ProductLineService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductLineController : Controller
  {
    private readonly IProductLineService _productLineService;

    public ProductLineController(IProductLineService productLineService)
    {
      _productLineService = productLineService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductLineRequestModel request)
    {
      await _productLineService.Create(request);
      return Accepted();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductLineRequestModel request)
    {
      await _productLineService.Update(id, request);
      return Accepted();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
      await _productLineService.Delete(id);
      return Accepted();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ProductLineResponseModel> GetById([FromRoute] Guid id)
    {
      return await _productLineService.GetById(id);
    }

    [HttpGet]
    public async Task<IList<ProductLineResponseModel>> GetAll()
    {
      return await _productLineService.GetAll();
    }

    [HttpGet]
    [Route("excelreport")]
    public async Task<FileResult> GenerateExcelReport()
    {
      byte[] file = await _productLineService.GenerateExcelReport();
      return File(file, "text/csv", "Relatorio-linha-produtos.csv");
    }

    [HttpPost]
    [Route("excelreport")]
    public async Task<IActionResult> ImportExcelReport([FromForm] IFormFile formFile)
    {
      var errors = await _productLineService.ReadExcelFile(formFile.OpenReadStream());
      if (string.IsNullOrEmpty(errors))
        return BadRequest(errors);
      return Ok("Importação realizada com sucesso");
    }
  }
}
