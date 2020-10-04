using MediatR;

namespace Application.Models.UserCommands
{
    public class CreateUserCommand : UserBaseModel, IRequest<UserResponseModel>
    {
    }
}
