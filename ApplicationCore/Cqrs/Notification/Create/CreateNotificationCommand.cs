using ApplicationCore.Bases;
using ApplicationCore.Dtos.Notification;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Notification.Create;
public record CreateNotificationCommand(NotificationInputDto Dto) : IRequest<NotificationDto>;

internal class CreateNotificationCommandHandler : BaseRequestHandler<CreateNotificationCommand, NotificationDto>
{
    public CreateNotificationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<NotificationDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        => await CreateAsync<NotificationEntity, NotificationDto>(request.Dto);
}