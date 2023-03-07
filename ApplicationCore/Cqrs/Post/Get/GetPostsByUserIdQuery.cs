using ApplicationCore.Bases;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Post.Get;

public record GetPostsByUserIdQuery(int UserId) : IRequest<IEnumerable<PostDto>>;

internal class GetPostsByUserIdQueryHandler : BaseRequestHandler, IRequestHandler<GetPostsByUserIdQuery, IEnumerable<PostDto>>
{
    public GetPostsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<IEnumerable<PostDto>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<PostEntity, PostDto>(x => x.UserId.Equals(request.UserId));
}