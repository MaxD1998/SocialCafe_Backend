using ApplicationCore.Bases;
using ApplicationCore.Dtos.Notification;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Notification.Get;
public record GetNotificationByIdQuery(Guid Id) : IRequest<NotificationDto>;

public class GetNotificationByIdQueryHandler : BaseRequestHandler<GetNotificationByIdQuery, NotificationDto>
{
    public GetNotificationByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<NotificationDto> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
        => await GetElementAsync<NotificationEntity, NotificationDto>(x => x.Id == request.Id);
}