using Api.Bases;
using ApplicationCore.Cqrs.Notification.Delete;
using ApplicationCore.Cqrs.Notification.Get;
using ApplicationCore.Cqrs.Notification.Update;
using ApplicationCore.Dtos.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class NotificationController : BaseApiController
{
    public NotificationController(IMediator mediator) : base(mediator)
    {
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync([FromRoute] Guid id)
        => await ApiResponseAsync<bool, DeleteNotificationCommand>(new(id));

    [HttpGet("{recipientId}")]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetsByRecipientIdAsync([FromRoute] Guid recipientId)
        => await ApiResponseAsync<IEnumerable<NotificationDto>, GetNotificationsByRecipientIdQuery>(new(recipientId));

    [HttpPut("{id}")]
    public async Task<ActionResult<NotificationDto>> UpdateAsync([FromRoute] Guid id, [FromBody] NotificationInputDto dto)
        => await ApiResponseAsync<NotificationDto, UpdateNotificationCommand>(new(id, dto));
}