using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
  public interface IServiceCrud<TRequestModel, TResponseModel>
  {
    Task Create(TRequestModel request);
    Task Update(Guid id, TRequestModel request);
    Task Delete(Guid id);
    Task<TResponseModel> GetById(Guid id);
    Task<IList<TResponseModel>> GetAll();
    Task ValidateEntityExistence(Guid entityId);
  }
}
