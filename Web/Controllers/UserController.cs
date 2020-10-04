using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Models.UserCommand;

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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserRequestModel request)
        {
            await _userService.Update(id, request);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _userService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<UserResponseModel> GetById([FromRoute] Guid id)
        {
            return await _userService.GetById(id);
        }

        [HttpGet]
        public async Task<IList<UserResponseModel>> GetAll()
        {
            return await _userService.GetAll();
        }
    }
}
