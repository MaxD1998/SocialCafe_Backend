using ApplicationCore.Cqrs.Hub.CreateUpdate;
using ApplicationCore.Cqrs.Hub.Delete;
using ApplicationCore.Cqrs.Hub.Get;
using ApplicationCore.Dtos.Hub;
using ApplicationCore.Extensions;
using AutoMapper;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Api.Bases;

[Authorize]
public abstract class BaseHub : Hub
{
    protected readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public BaseHub(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    protected abstract HubType Type { get; }

    protected Guid UserId => Context.GetHttpContext().User.GetUserId();

    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var dto = new HubInputDto()
        {
            ConnectionId = connectionId,
            Type = Type,
            UserId = UserId
        };

        await _mediator.Send(new CreateUpdateHubCommand(dto));
    }

    public override async Task OnDisconnectedAsync(Exception exception)
        => await _mediator.Send(new DeleteHubByUserIdAndTypeCommand(UserId, Type));

    protected async Task Send<T>(Guid userId, string method, T result)
    {
        var userIds = new[] { userId };
        await Send(userIds, method, result);
    }

    protected async Task Send<T>(IEnumerable<Guid> userIds, string method, T result)
    {
        var hubs = await _mediator.Send(new GetHubsByUserIdsAndTypeQuery(userIds, Type));
        var connectionIds = hubs.Select(x => x.ConnectionId);

        await Clients.Clients(connectionIds).SendAsync(method, result);
    }
}