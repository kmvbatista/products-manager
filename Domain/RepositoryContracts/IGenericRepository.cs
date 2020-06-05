using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IGenericRepository<TEntity>
      where TEntity : BaseEntity
  {
    Task<IList<TEntity>> GetAll();
    Task<TEntity> GetById(Guid id);
    Task Create(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(Guid id);
    Task<bool> ValidateEntityExistence(Guid entityId);
  }
}
