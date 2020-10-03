using MediatR;
using System;

namespace Application.Models.UserCommands
{
    public class UpdateUserCommand : UserBaseModel, IRequest<UserResponseModel>
    {
        public Guid UserId { get; set; }
    }
}
