using Application.Models.ProductCategoryModels;
using Application.Services;
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
  public class ProductCategoryController : Controller
  {
    private readonly IProductCategoryService _productCategoryService;

    public ProductCategoryController(IProductCategoryService categoriaProdutoService)
    {
      _productCategoryService = categoriaProdutoService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCategoryRequestModel request)
    {
      await _productCategoryService.Create(request);
      return Accepted();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductCategoryRequestModel request)
    {
      await _productCategoryService.Update(id, request);
      return Accepted();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
      await _productCategoryService.Delete(id);
      return Accepted();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ProductCategoryResponseModel> GetById([FromRoute] Guid id)
    {
      return await _productCategoryService.GetById(id);
    }

    [HttpGet]
    public async Task<IList<ProductCategoryResponseModel>> GetAll()
    {
      return await _productCategoryService.GetAll();
    }

    [HttpGet]
    [Route("excelreport")]
    public async Task<FileResult> GenerateExcelReport()
    {
      byte[] file = await _productCategoryService.GenerateExcelReport();
      return File(file, "text/csv", "Relatorio-categoria-produtos.csv");
    }

    [HttpPost]
    [Route("excelreport")]
    public async Task<IActionResult> ImportExcelReport([FromForm] IFormFile formFile)
    {
      var errors = await _productCategoryService.ReadExcelFile(formFile.OpenReadStream());
      if (string.IsNullOrEmpty(errors))
        return BadRequest(errors);
      return Ok("Importação realizada com sucesso");
    }
  }
}
