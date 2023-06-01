using ApplicationCore.Bases;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Post.Get;

public record GetPostsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<PostDto>>;

internal class GetPostsByUserIdQueryHandler : BaseRequestHandler<GetPostsByUserIdQuery, IEnumerable<PostDto>>
{
    public GetPostsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<PostDto>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<PostEntity, PostDto>(x => x.UserId == request.UserId);
}