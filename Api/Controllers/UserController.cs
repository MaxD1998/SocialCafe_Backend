using Api.Bases;
using ApplicationCore.Cqrs.User.Delete;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));

            return result ? Ok() : NotFound();
        }

        [HttpGet("ByEmail")]
        public async Task<ActionResult<UserDto>> GetUserByEmailAsync([FromQuery] string email)
        {
            var results = await _mediator.Send(new GetUserByEmailQuery(email));

            return Ok(results);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync()
        {
            var results = await _mediator.Send(new GetUsersQuery());

            return Ok(results);
        }

        [HttpGet("ByFirstNameAndLastName")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByFirstNameAndLastNameAsync([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var results = await _mediator.Send(new GetUsersByFirstNameAndLastNameQuery(firstName, lastName));

            return Ok(results);
        }
    }
}