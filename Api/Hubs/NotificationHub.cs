using Api.Bases;
using ApplicationCore.Cqrs.Notification.Create;
using ApplicationCore.Dtos.Notification;
using AutoMapper;
using Domain.Enums;
using MediatR;

namespace Api.Hubs;

public class NotificationHub : BaseHub
{
    public NotificationHub(IMapper mapper, IMediator mediator) : base(mapper, mediator)
    {
    }

    protected override HubType Type => HubType.Notification;

    public async Task CreateNotificationAsync(NotificationInputDto dto)
    {
        var result = await _mediator.Send(new CreateNotificationCommand(dto));

        await Send(result.RecipientId, "GetNotification", result);
    }
}