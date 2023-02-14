using Api.Bases;
using ApplicationCore.Cqrs.ConversationMember.Get;
using ApplicationCore.Cqrs.Message.Create;
using ApplicationCore.Dtos.Message;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class SocialChatHub : BaseHub
{
    private readonly IMediator _mediator;

    public SocialChatHub(IMediator mediator, IChatService messageService) : base(messageService)
    {
        _mediator = mediator;
    }

    public async Task CreateMessageAsync(MessageInputDto dto)
    {
        var result = await _mediator.Send(new CreateMessageCommand(dto));
        var conversationMembers = await _mediator.Send(new GetConverastionMembersByConversationIdCommand(result.ConversationId));
        var connectionIds = conversationMembers
            .Select(x => x.User.ConnectionId)
            .Where(x => x != null);

        await Clients.Clients(connectionIds).SendAsync("RecieveMessageHandler", result);
    }
}