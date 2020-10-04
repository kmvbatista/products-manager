using Application.Models.UserCommands;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
        : Controller
    {
        private readonly IUserService _userService;
        private readonly IMediator _mediator;
        public UserController(IMediator mediator, IUserService userService)
        {
            _userService = userService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateUserCommand request)
        {
            request.UserId = id;
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<UserResponseModel> GetById([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetUserByIdCommand(id));
        }

        [HttpGet]
        public async Task<IList<UserResponseModel>> GetAll()
        {
            return await _mediator.Send(new GetAllUsersCommand());
        }
    }
}
