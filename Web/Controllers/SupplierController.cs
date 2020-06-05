using Application.Models.SupplierModels;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SupplierController
      : Controller
  {
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
      _supplierService = supplierService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SupplierRequestModel request)
    {
      await _supplierService.Create(request);
      return Accepted();
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] SupplierRequestModel request)
    {
      await _supplierService.Update(id, request);
      return Accepted();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
      await _supplierService.Delete(id);
      return Accepted();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<SupplierResponseModel> GetById([FromRoute] Guid id)
    {
      return await _supplierService.GetById(id);
    }

    [HttpGet]
    public async Task<IList<SupplierResponseModel>> GetAll()
    {
      return await _supplierService.GetAll();
    }
  }
}
