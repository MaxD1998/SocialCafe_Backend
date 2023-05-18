using ApplicationCore.Bases;
using ApplicationCore.Dtos.Friend;
using ApplicationCore.Dtos.User;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Cqrs.User.Get;

public record GetUsersByNamesExceptUserFriendsQuery(string FirstName, string LastName) : IRequest<IEnumerable<InviteUserDto>>;

internal class GetUsersByNamesExceptUserFriendsQueryHandler : BaseRequestHandler<GetUsersByNamesExceptUserFriendsQuery, IEnumerable<InviteUserDto>>
{
    private readonly Guid _userId;

    public GetUsersByNamesExceptUserFriendsQueryHandler(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _userId = httpContextAccessor.HttpContext.User.GetUserId();
    }

    public override async Task<IEnumerable<InviteUserDto>> Handle(GetUsersByNamesExceptUserFriendsQuery request, CancellationToken cancellationToken)
    {
        var ids = await GetFriendIdsAsync();
        var results = await GetUsersAsync(request, ids);
        await SetNotificationDataAsync(results);

        return results;
    }

    private async Task<IEnumerable<Guid>> GetFriendIdsAsync()
    {
        var friends = await GetElementsAsync<FriendEntity, FriendDto>(x => x.InviterId == _userId || x.RecipientId == _userId, true);
        var ids = friends.Select(x => x.UserId);

        return ids.Concat(new[] { _userId });
    }

    private async Task<IEnumerable<InviteUserDto>> GetUsersAsync(GetUsersByNamesExceptUserFriendsQuery request, IEnumerable<Guid> friendIds)
        => await GetElementsAsync<UserEntity, InviteUserDto>(x => !friendIds.Contains(x.Id)
            && (x.FirstName.ToLower() == request.FirstName.ToLower()
            || x.LastName.ToLower() == request.LastName.ToLower()
            || x.FirstName.ToLower() == request.LastName.ToLower()
            || x.LastName.ToLower() == request.FirstName.ToLower()));

    private async Task SetNotificationDataAsync(IEnumerable<InviteUserDto> users)
    {
        var invitations = await _unitOfWork.BaseRepository.GetElementsAsync<NotificationEntity>(
            x => (x.UserId == _userId || x.RecipientId == _userId)
                && x.Type == Domain.Enums.NotificationType.FriendInvitation,
            true);

        foreach (var user in users)
        {
            var invitation = invitations.FirstOrDefault(x => x.UserId == user.Id || x.RecipientId == user.Id);
            user.NotificationId = invitation?.Id;
            user.Type = invitation is null
                ? InvitationType.None
                : invitation.UserId == user.Id
                    ? InvitationType.Inviter
                    : InvitationType.Invited;
        }
    }
}