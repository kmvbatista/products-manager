using MediatR;
using System.Collections.Generic;

namespace Application.Models.UserCommands
{
    public struct GetAllUsersCommand : IRequest<List<UserResponseModel>>
    {
    }
}
