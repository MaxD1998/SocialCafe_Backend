using Api.Bases;
using ApplicationCore.Cqrs.Message.Get;
using ApplicationCore.Dtos.Message;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class MessageController : BaseApiController
{
    public MessageController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("ConversationId/{conversationId}")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetsByConversationIdAsync([FromRoute] int conversationId)
        => await ApiResponseAsync<IEnumerable<MessageDto>, GetMessagesByConversationIdQuery>(new(conversationId));
}