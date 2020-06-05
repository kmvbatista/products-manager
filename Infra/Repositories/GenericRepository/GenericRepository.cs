using Domain.Entity;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Repositories.GenericRepository
{
  public class GenericRepository<TEntity>
      : IGenericRepository<TEntity> where TEntity : BaseEntity
  {
    public readonly MainContext _dbContext;

    public GenericRepository(MainContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task Create(TEntity entity)
    {
      await _dbContext.Set<TEntity>().AddAsync(entity);
      await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
      var entity = await GetById(id);
      _dbContext.Set<TEntity>().Remove(entity);
      await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<IList<TEntity>> GetAll()
    {
      return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity> GetById(Guid id)
    {
      return await _dbContext.Set<TEntity>()
          .AsNoTracking()
          .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task Update(TEntity entity)
    {
      _dbContext.Set<TEntity>().Update(entity);
      await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ValidateEntityExistence(Guid entityId)
    {
      var entityExists = await _dbContext.Set<TEntity>().AnyAsync(x => x.Id == entityId);
      if (!entityExists)
        return false;
      return true;
    }
  }
}
