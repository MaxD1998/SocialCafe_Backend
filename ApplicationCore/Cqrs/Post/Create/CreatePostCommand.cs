using ApplicationCore.Bases;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Post.Create;

public record CreatePostCommand(PostInputDto Dto) : IRequest<PostDto>;

internal class CreatePostCommandHandler : BaseRequestHandler, IRequestHandler<CreatePostCommand, PostDto>
{
    public CreatePostCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        => await CreateAsync<PostEntity, PostDto>(request.Dto);
}