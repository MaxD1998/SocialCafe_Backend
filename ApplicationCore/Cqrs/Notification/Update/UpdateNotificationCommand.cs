using ApplicationCore.Bases;
using ApplicationCore.Dtos.Notification;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Notification.Update;
public record UpdateNotificationCommand(Guid Id, NotificationInputDto Dto) : IRequest<NotificationDto>;

internal class UpdateNotificationCommandHanlder : BaseRequestHandler<UpdateNotificationCommand, NotificationDto>
{
    public UpdateNotificationCommandHanlder(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<NotificationDto> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
        => await UpdateAsync<NotificationEntity, NotificationDto>(request.Id, request.Dto);
}