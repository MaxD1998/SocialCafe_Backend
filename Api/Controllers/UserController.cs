using Api.Bases;
using ApplicationCore.Cqrs.User.Delete;
using ApplicationCore.Cqrs.User.Get;
using ApplicationCore.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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
    public async Task<ActionResult<UserDto>> GetByEmailAsync([FromQuery] string email)
        => await ApiResponseAsync<UserDto, GetUserByEmailQuery>(new(email));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetsAsync()
        => await ApiResponseAsync<IEnumerable<UserDto>, GetUsersQuery>(new());

    [HttpGet("ByFirstNameAndLastName")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetsByFirstNameAndLastNameAsync([FromQuery] string firstName, [FromQuery] string lastName)
        => await ApiResponseAsync<IEnumerable<UserDto>, GetUsersByFirstNameAndLastNameQuery>(new(firstName, lastName));
}