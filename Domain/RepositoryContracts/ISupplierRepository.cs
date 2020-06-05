using Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infra.Repositories.Supplier
{
  public interface ISupplierRepository : IGenericRepository<Domain.Entity.Supplier>
  {
    Task<bool> ExistsById(Guid Id);
  }
}
