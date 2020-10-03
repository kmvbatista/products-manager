using MediatR;
using System;

namespace Application.Models.UserCommands
{
    public struct DeleteUserCommand : IRequest
    {
        public Guid UserId { get; set; }
        public DeleteUserCommand(Guid Id)
        {
            UserId = Id;
        }
    }
}
