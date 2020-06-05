using Domain.Interfaces;
using Infra.Context;
using Infra.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositories.ProductLine
{
  public class ProductLineRepository : GenericRepository<Domain.Entity.ProductLine>, IProductLineRepository
  {
    public ProductLineRepository(MainContext dbContext) : base(dbContext)
    {

    }

    public override async Task<IList<Domain.Entity.ProductLine>> GetAll()
    {
      return await _dbContext.Set<Domain.Entity.ProductLine>()
          .Include(cat => cat.ProductCategory)
          .ToListAsync();
    }

    public override async Task<Domain.Entity.ProductLine> GetById(Guid Id)
    {
      return await _dbContext.Set<Domain.Entity.ProductLine>()
          .Include(cat => cat.ProductCategory)
          .FirstOrDefaultAsync(e => e.Id == Id);
    }
  }
}
