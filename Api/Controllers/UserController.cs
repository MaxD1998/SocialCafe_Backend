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
    public async Task<ActionResult<bool>> DeleteAsync(int id)
        => await ApiResponseAsync<bool, DeleteUserCommand>(new(id));

    [HttpGet("Email")]
    public async Task<ActionResult<UserDto>> GetByEmailAsync([FromQuery] string email)
        => await ApiResponseAsync<UserDto, GetUserByEmailQuery>(new(email));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetsAsync()
        => await ApiResponseAsync<IEnumerable<UserDto>, GetUsersQuery>(new());

    [HttpGet("NamesExceptUserFriends")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetsByNamesExceptUserFriendsAsync([FromQuery] string firstName, [FromQuery] string lastName)
        => await ApiResponseAsync<IEnumerable<UserDto>, GetUsersByNamesExceptUserFriendsQuery>(new(firstName ?? string.Empty, lastName ?? string.Empty));
}