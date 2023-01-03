using Api.Bases;
using ApplicationCore.Cqrs.Conversation.Create;
using ApplicationCore.Cqrs.Conversation.Get;
using ApplicationCore.Cqrs.ConversationMember.Create;
using ApplicationCore.Dtos.Conversation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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

    [HttpPost("Extend")]
    public async Task<ActionResult<ConversationDto>> CreateConversationExtendAsync([FromBody] ConversationInputExtendDto dto)
    {
        var conversation = await _mediator.Send(new CreateConversationCommand(dto));

        dto.ConversationMembers.ForEach(x => x.ConversationId = conversation.Id);

        await _mediator.Send(new CreateConversationMembersCommand(dto.ConversationMembers));

        var result = await _mediator.Send(new GetConversationByIdQuery(conversation.Id));

        return Ok(result);
    }

    [HttpGet("Id/{id}")]
    public async Task<ActionResult<IEnumerable<ConversationDto>>> GetConversationByIdAsync([FromRoute] int id)
    {
        var result = await _mediator.Send(new GetConversationByIdQuery(id));

        return Ok(result);
    }

    [HttpGet("UserId/{userId}")]
    public async Task<ActionResult<IEnumerable<ConversationDto>>> GetConversationsByUserIdAsync([FromRoute] int userId)
    {
        var result = await _mediator.Send(new GetConversationsByUserIdQuery(userId));

        return Ok(result);
    }
}