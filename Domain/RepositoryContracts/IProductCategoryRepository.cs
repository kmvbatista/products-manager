using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositories.ProductCategory
{
  public interface IProductCategoryRepository : IGenericRepository<Domain.Entity.ProductCategory>
  {
    Task AddCategoryRange(List<Domain.Entity.ProductCategory> productCategories);
    Task UpdateCategoryRange(List<Domain.Entity.ProductCategory> productCategories);
  }
}
