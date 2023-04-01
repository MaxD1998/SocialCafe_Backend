using ApplicationCore.Bases;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Notification.Delete;
public record DeleteNotificationCommand(Guid Id) : IRequest<bool>;

internal class DeleteNotificationCommandHandler : BaseRequestHandler<DeleteNotificationCommand, bool>
{
    public DeleteNotificationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
        => await DeleteAsync<NotificationEntity>(x => x.Id == request.Id);
}