using ApplicationCore.Bases;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Post.Get;

public record GetPostsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<PostDto>>;

internal class GetPostsByUserIdQueryHandler : BaseRequestHandler, IRequestHandler<GetPostsByUserIdQuery, IEnumerable<PostDto>>
{
    public GetPostsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<IEnumerable<PostDto>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var ids = await GetElementsAsync<FriendEntity, Guid>(
            x => x.InviterId == request.UserId || x.RecipientId == request.UserId,
            x => x.InviterId != request.UserId ? x.InviterId : x.RecipientId,
            true);

        ids.Concat(new[] { request.UserId });

        return await GetElementsAsync<PostEntity, PostDto>(x => ids.Contains(x.UserId));
    }
}