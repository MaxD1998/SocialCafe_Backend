using Api.Bases;
using ApplicationCore.Cqrs.User.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class UserController : BaseApiController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("ByEmail")]
        public async Task<IActionResult> GetUserByEmailAsync([FromQuery] string email)
        {
            var results = await Mediator.Send(new GetUsersQuery());

            return Ok(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var results = await Mediator.Send(new GetUsersQuery());

            return Ok(results);
        }
    }
}