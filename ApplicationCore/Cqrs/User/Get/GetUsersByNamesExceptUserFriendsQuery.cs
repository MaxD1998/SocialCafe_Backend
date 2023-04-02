using ApplicationCore.Bases;
using ApplicationCore.Dtos.Friend;
using ApplicationCore.Dtos.User;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Cqrs.User.Get;

public record GetUsersByNamesExceptUserFriendsQuery(string FirstName, string LastName) : IRequest<IEnumerable<UserDto>>;

internal class GetUsersByNamesExceptUserFriendsQueryHandler : BaseRequestHandler<GetUsersByNamesExceptUserFriendsQuery, IEnumerable<UserDto>>
{
    private readonly Guid _userId;

    public GetUsersByNamesExceptUserFriendsQueryHandler(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _userId = httpContextAccessor.HttpContext.User.GetUserId();
    }

    public override async Task<IEnumerable<UserDto>> Handle(GetUsersByNamesExceptUserFriendsQuery request, CancellationToken cancellationToken)
    {
        var friends = await GetElementsAsync<FriendEntity, FriendDto>(x => x.InviterId == _userId || x.RecipientId == _userId, true);
        var ids = friends.Select(x => x.UserId);

        ids = ids.Concat(new[] { _userId });

        return await GetElementsAsync<UserEntity, UserDto>(x => !ids.Contains(x.Id)
            && (x.FirstName.ToLower() == request.FirstName.ToLower()
            || x.LastName.ToLower() == request.LastName.ToLower()
            || x.FirstName.ToLower() == request.LastName.ToLower()
            || x.LastName.ToLower() == request.FirstName.ToLower()));
    }
}