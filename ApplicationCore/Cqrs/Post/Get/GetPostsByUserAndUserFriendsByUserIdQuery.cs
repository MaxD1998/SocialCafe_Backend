using ApplicationCore.Bases;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Post.Get;

public record GetPostsByUserAndUserFriendsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<PostDto>>;

internal class GetPostsByUserAndUserFriendsByUserIdQueryHandler : BaseRequestHandler, IRequestHandler<GetPostsByUserAndUserFriendsByUserIdQuery, IEnumerable<PostDto>>
{
    public GetPostsByUserAndUserFriendsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<IEnumerable<PostDto>> Handle(GetPostsByUserAndUserFriendsByUserIdQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<PostEntity, PostDto>(x => x.UserId.Equals(request.UserId));
}