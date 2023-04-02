using ApplicationCore.Bases;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace ApplicationCore.Cqrs.Hub.Delete;
public record DeleteHubByUserIdAndTypeCommand(Guid UserId, HubType Type) : IRequest<bool>;

internal class DeleteHubByUserIdAndTypeCommandHandler : BaseRequestHandler<DeleteHubByUserIdAndTypeCommand, bool>
{
    public DeleteHubByUserIdAndTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<bool> Handle(DeleteHubByUserIdAndTypeCommand request, CancellationToken cancellationToken)
        => await DeleteAsync<HubEntity>(x => x.UserId == request.UserId && x.Type == request.Type);
}