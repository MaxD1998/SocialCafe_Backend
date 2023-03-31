using ApplicationCore.Bases;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.User.Delete;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;

internal class DeleteUserCommandHandler : BaseRequestHandler, IRequestHandler<DeleteUserCommand, bool>
{
    public DeleteUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        => await DeleteAsync<UserEntity>(x => x.Id == request.Id);
}