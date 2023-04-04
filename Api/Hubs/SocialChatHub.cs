using Api.Bases;
using ApplicationCore.Cqrs.ConversationMember.Get;
using ApplicationCore.Cqrs.Message.Create;
using ApplicationCore.Dtos.Message;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Api.Hubs;

public class SocialChatHub : BaseHub
{
    public SocialChatHub(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    {
    }

    protected override HubType Type => HubType.SocialCafe;

    public async Task CreateMessageAsync(MessageInputDto dto)
    {
        var result = await _mediator.Send(new CreateMessageCommand(dto));
        var conversationMembers = await _mediator.Send(new GetConverastionMembersByConversationIdCommand(result.ConversationId));
        var userIds = conversationMembers.Select(x => x.UserId);

        await Send(userIds, "GetMessage", result);
    }
}