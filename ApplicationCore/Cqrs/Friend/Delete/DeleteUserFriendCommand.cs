using ApplicationCore.Bases;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Cqrs.Friend.Delete;

public record DeleteUserFriendCommand(Guid Id) : IRequest<bool>;

internal class DeleteUserFriendCommandHandler : BaseRequestHandler<DeleteUserFriendCommand, bool>
{
    private readonly Guid _userId;

    public DeleteUserFriendCommandHandler(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _userId = httpContextAccessor.HttpContext.User.GetUserId();
    }

    public override async Task<bool> Handle(DeleteUserFriendCommand request, CancellationToken cancellationToken)
        => await DeleteAsync<FriendEntity>(x => x.Id == request.Id && (x.InviterId == _userId || x.RecipientId == _userId));
}