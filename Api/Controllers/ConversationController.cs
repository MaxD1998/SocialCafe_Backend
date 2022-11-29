using Api.Bases;
using ApplicationCore.Cqrs.Conversation.Create;
using ApplicationCore.Cqrs.Conversation.Get;
using ApplicationCore.Dtos.Conversation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ConversationController : BaseApiController
    {
        public ConversationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<ConversationDto>> CreateConversationAsync([FromBody] ConversationInputDto dto)
        {
            var result = await _mediator.Send(new CreateConversationCommand(dto));

            return Ok(result);
        }

        [HttpGet("UserId/{userId}")]
        public async Task<ActionResult<IEnumerable<ConversationDto>>> GetConversationsByUserIdAsync([FromRoute] int userId)
        {
            var result = await _mediator.Send(new GetConversationsByUserIdQuery(userId));

            return Ok(result);
        }
    }
}