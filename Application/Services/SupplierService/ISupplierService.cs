using Application.Models.SupplierModels;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
  public interface ISupplierService : IServiceCrud<SupplierRequestModel, SupplierResponseModel>
  {
    Task<bool> ExistsById(Guid id);
    Task<IList<Supplier>> GetAllRaw();
  }
}
