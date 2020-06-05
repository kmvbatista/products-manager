using System.Threading.Tasks;
using Infra.Context;
using Infra.Repositories.GenericRepository;
using System;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.Supplier
{
  public class SupplierRepository : GenericRepository<Domain.Entity.Supplier>, ISupplierRepository
  {

    public SupplierRepository(MainContext dbContext) : base(dbContext)
    {

    }

    public async Task<bool> ExistsById(Guid Id)
    {
      return await _dbContext.Set<Domain.Entity.Supplier>().AnyAsync(x => x.Id == Id);
    }
  }
}
