using Application.Models;

namespace Application.Services
{
  public interface IUserService : IServiceCrud<UserRequestModel, UserResponseModel>
  {
  }
}
