using MediatR;
using System;

namespace Application.Models.UserCommands
{
    public struct GetUserByIdCommand : IRequest<UserResponseModel>
    {
        public GetUserByIdCommand(Guid id)
        {
            UserId = id;
        }
        public Guid UserId { get; set; }
    }
}
