using ApplicationCore.Bases;
using ApplicationCore.Dtos.Friend;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Friend.Get;

public record GetFriendsByUserIdQuery(int UserId) : IRequest<IEnumerable<FriendDto>>;

public class GetFriendsByUserIdQueryHandler : BaseRequestHandler, IRequestHandler<GetFriendsByUserIdQuery, IEnumerable<FriendDto>>
{
    public GetFriendsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<IEnumerable<FriendDto>> Handle(GetFriendsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var friends = await _unitOfWork.BaseRepository
            .GetElementsAsync<FriendEntity>(x => x.InviterId == request.UserId || x.RecipientId == request.UserId);
        var users = await _unitOfWork.BaseRepository
            .GetElementsAsync<UserEntity>(x => !x.Id.Equals(request.UserId)
                && (friends.Select(x => x.InviterId).Contains(x.Id)
                    || friends.Select(x => x.RecipientId).Contains(x.Id)));

        return MergeData(friends, users);
    }

    private IEnumerable<FriendDto> MergeData(IEnumerable<FriendEntity> friends, IEnumerable<UserEntity> users)
    {
        foreach (var friend in friends)
        {
            var user = users.First(x => x.Id.Equals(friend.InviterId) || x.Id.Equals(friend.RecipientId));

            yield return new FriendDto()
            {
                Id = friend.Id,
                User = _mapper.Map<UserDto>(user),
                UserId = user.Id,
            };
        }
    }
}