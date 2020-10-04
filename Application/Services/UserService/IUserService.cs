using Application.Models.UserCommands;

namespace Application.Services
{
    public interface IUserService : IServiceCrud<UserRequestModel, UserResponseModel>
  {
  }
}
