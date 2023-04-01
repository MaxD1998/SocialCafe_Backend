using ApplicationCore.Bases;
using ApplicationCore.Dtos.Notification;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace ApplicationCore.Cqrs.Notification.Get;

public record GetNotificationByRecipientIdQuery(Guid RecipientId) : IRequest<IEnumerable<NotificationDto>>;

internal class GetNotificationByRecipientIdQueryRequest : BaseRequestHandler, IRequestHandler<GetNotificationByRecipientIdQuery, IEnumerable<NotificationDto>>
{
    public GetNotificationByRecipientIdQueryRequest(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public Task<IEnumerable<NotificationDto>> Handle(GetNotificationByRecipientIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}