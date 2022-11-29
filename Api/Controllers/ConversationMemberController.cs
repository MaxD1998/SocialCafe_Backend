using Api.Bases;
using ApplicationCore.Cqrs.ConversationMember.Create;
using ApplicationCore.Dtos.ConversationMember;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ConversationMemberController : BaseApiController
    {
        public ConversationMemberController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<ConversationMemberDto>> CreateConversationMemberAsync(ConversationMemberDto dto)
        {
            var result = await _mediator.Send(new CreateConversationMemberCommand(dto));

            return Ok(result);
        }

        [HttpPost("Range")]
        public async Task<ActionResult<IEnumerable<ConversationMemberDto>>> CreateConversationMembersAsync(IEnumerable<ConversationMemberDto> dtos)
        {
            var results = await _mediator.Send(new CreateConversationMembersCommand(dtos));

            return Ok(results);
        }
    }
}