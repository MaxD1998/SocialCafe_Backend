using Api.Bases;
using ApplicationCore.Cqrs.ConversationMember.Create;
using ApplicationCore.Dtos.ConversationMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ConversationMemberController : BaseApiController
{
    public ConversationMemberController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<ActionResult<ConversationMemberDto>> CreateAsync(ConversationMemberDto dto)
        => await ApiResponseAsync<ConversationMemberDto, CreateConversationMemberCommand>(new(dto));

    [HttpPost("Range")]
    public async Task<ActionResult<IEnumerable<ConversationMemberDto>>> CreatesAsync(IEnumerable<ConversationMemberDto> dtos)
        => await ApiResponseAsync<IEnumerable<ConversationMemberDto>, CreateConversationMembersCommand>(new(dtos));
}