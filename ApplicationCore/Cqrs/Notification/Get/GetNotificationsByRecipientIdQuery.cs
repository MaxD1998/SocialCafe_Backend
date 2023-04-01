using ApplicationCore.Bases;
using ApplicationCore.Dtos.Notification;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Notification.Get;

public record GetNotificationsByRecipientIdQuery(Guid RecipientId) : IRequest<IEnumerable<NotificationDto>>;

internal class GetNotificationsByRecipientIdQueryHandler : BaseRequestHandler<GetNotificationsByRecipientIdQuery, IEnumerable<NotificationDto>>
{
    public GetNotificationsByRecipientIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<NotificationDto>> Handle(GetNotificationsByRecipientIdQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<NotificationEntity, NotificationDto>(x => x.RecipientId == request.RecipientId);
}