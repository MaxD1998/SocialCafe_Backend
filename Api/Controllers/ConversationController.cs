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
    public async Task<ActionResult<ConversationDto>> CreateAsync([FromBody] ConversationInputDto dto)
        => await ApiResponseAsync<ConversationDto, CreateConversationCommand>(new(dto));

    [HttpPost("Extend")]
    public async Task<ActionResult<ConversationDto>> CreateExtendAsync([FromBody] ConversationInputExtendDto dto)
    {
        var conversation = await _mediator.Send(new CreateConversationCommand(dto));

        dto.ConversationMembers.ForEach(x => x.ConversationId = conversation.Id);

        await _mediator.Send(new CreateConversationMembersCommand(dto.ConversationMembers));

        var result = await _mediator.Send(new GetConversationByIdQuery(conversation.Id));

        return Ok(result);
    }

    [HttpGet("Id/{id}")]
    public async Task<ActionResult<ConversationDto>> GetByIdAsync([FromRoute] int id)
        => await ApiResponseAsync<ConversationDto, GetConversationByIdQuery>(new(id));

    [HttpGet("UserId/{userId}")]
    public async Task<ActionResult<IEnumerable<ConversationDto>>> GetsByUserIdAsync([FromRoute] int userId)
        => await ApiResponseAsync<IEnumerable<ConversationDto>, GetConversationsByUserIdQuery>(new(userId));
}