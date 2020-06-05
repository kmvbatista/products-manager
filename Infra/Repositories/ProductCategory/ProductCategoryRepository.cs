using Infra.Repositories.GenericRepository;
using Infra.Context;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories.ProductCategory
{
  public class ProductCategoryRepository : GenericRepository<Domain.Entity.ProductCategory>, IProductCategoryRepository
  {
    public ProductCategoryRepository(MainContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IList<Domain.Entity.ProductCategory>> GetAll()
    {
      return await _dbContext.Set<Domain.Entity.ProductCategory>()
          .Include(cat => cat.Supplier)
          .ToListAsync();
    }

    public async Task AddCategoryRange(List<Domain.Entity.ProductCategory> productCategories)
    {
      await _dbContext.Set<Domain.Entity.ProductCategory>().AddRangeAsync(productCategories);
      await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCategoryRange(List<Domain.Entity.ProductCategory> productCategories)
    {
      _dbContext.Set<Domain.Entity.ProductCategory>().UpdateRange(productCategories);
      await _dbContext.SaveChangesAsync();
    }
  }
}
