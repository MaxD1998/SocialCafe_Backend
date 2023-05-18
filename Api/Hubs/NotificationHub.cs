using Api.Bases;
using ApplicationCore.Cqrs.Notification.Create;
using ApplicationCore.Cqrs.Notification.Delete;
using ApplicationCore.Cqrs.Notification.Get;
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

    public async Task<Guid> CreateNotificationAsync(NotificationInputDto dto)
    {
        var result = await _mediator.Send(new CreateNotificationCommand(dto));

        await Send(result.RecipientId, "AddNotification", result);

        return result.Id;
    }

    public async Task<bool> RemoveNotificationAsync(Guid id)
    {
        var notification = await _mediator.Send(new GetNotificationByIdQuery(id));

        if (notification is null)
            return false;

        var result = await _mediator.Send(new DeleteNotificationCommand(id));

        if (result)
            await Send(notification.RecipientId, "RemoveNotification", notification.Id);

        return result;
    }
}