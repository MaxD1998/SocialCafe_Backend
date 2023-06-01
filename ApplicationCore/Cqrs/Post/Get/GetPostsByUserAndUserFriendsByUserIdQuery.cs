using ApplicationCore.Bases;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Post.Get;

public record GetPostsByUserAndUserFriendsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<PostDto>>;

internal class GetPostsByUserAndUserFriendsByUserIdQueryHandler : BaseRequestHandler<GetPostsByUserAndUserFriendsByUserIdQuery, IEnumerable<PostDto>>
{
    public GetPostsByUserAndUserFriendsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<PostDto>> Handle(GetPostsByUserAndUserFriendsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var ids = await GetElementsAsync<FriendEntity, Guid>(
            x => x.InviterId == request.UserId || x.RecipientId == request.UserId,
            x => x.InviterId != request.UserId ? x.InviterId : x.RecipientId,
            true);

        ids = ids.Concat(new[] { request.UserId });

        return await GetElementsAsync<PostEntity, PostDto>(x => ids.Contains(x.UserId));
    }
}