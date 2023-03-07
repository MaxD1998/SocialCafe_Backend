using Api.Bases;
using ApplicationCore.Cqrs.Conversation.Create;
using ApplicationCore.Cqrs.Conversation.Get;
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

    [HttpGet("Id/{id}")]
    public async Task<ActionResult<ConversationDto>> GetByIdAsync([FromRoute] Guid id)
        => await ApiResponseAsync<ConversationDto, GetConversationByIdQuery>(new(id));

    [HttpGet("UserId/{userId}")]
    public async Task<ActionResult<IEnumerable<ConversationDto>>> GetsByUserIdAsync([FromRoute] Guid userId)
        => await ApiResponseAsync<IEnumerable<ConversationDto>, GetConversationsByUserIdQuery>(new(userId));
}