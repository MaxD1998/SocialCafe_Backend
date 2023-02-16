using Api.Extensions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.Bases;

[Authorize]
public abstract class BaseHub : Hub
{
    protected readonly IChatService _messageService;

    public BaseHub(IChatService messageService)
    {
        _messageService = messageService;
    }

    protected int UserId => Context.GetHttpContext().User.GetUserId();

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;

        await _messageService.UpdateUserConnectionId(UserId, connectionId);

        var connectionIds = await _messageService.GetConnectionIds(UserId);

        Clients.Clients(connectionIds);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await _messageService.UpdateUserConnectionId(UserId, null);

        var connectionIds = await _messageService.GetConnectionIds(UserId);

        Clients.Clients(connectionIds);
    }
}