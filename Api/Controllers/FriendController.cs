using Api.Bases;
using ApplicationCore.Cqrs.Friend.Create;
using ApplicationCore.Cqrs.Friend.Get;
using ApplicationCore.Dtos.Friend;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class FriendController : BaseApiController
    {
        public FriendController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<FriendDto>> CreateFriendAsync([FromBody] FriendInputDto dto)
        {
            var result = await _mediator.Send(new CreateFriendCommand(dto));

            return Ok(result);
        }

        [HttpGet("UserId/{userId}")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> CreateFriendsByUserIdAsync([FromRoute] int userId)
        {
            var result = await _mediator.Send(new GetFriendsByUserIdQuery(userId));

            return Ok(result);
        }
    }
}