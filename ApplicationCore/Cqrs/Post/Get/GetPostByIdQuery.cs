using ApplicationCore.Bases;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Post.Get;

public record GetPostByIdQuery(int Id) : IRequest<PostDto>;

public class GetPostByIdQueryHandler : BaseRequestHandler, IRequestHandler<GetPostByIdQuery, PostDto>
{
    public GetPostByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        => await GetElementAsync<PostEntity, PostDto>(x => x.UserId.Equals(request.Id));
}