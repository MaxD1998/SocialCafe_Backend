using Api.Bases;
using ApplicationCore.Cqrs.User.Get;
using ApplicationCore.Dtos.User;
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
        public async Task<ActionResult<UserDto>> GetUserByEmailAsync([FromQuery] string email)
        {
            var results = await Mediator.Send(new GetUserByEmailQuery(email));

            return Ok(results);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync()
        {
            var results = await Mediator.Send(new GetUsersQuery());

            return Ok(results);
        }
    }
}