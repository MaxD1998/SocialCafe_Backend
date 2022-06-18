using ApplicationCore.Bases;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Post.Create
{
    public record CreatePostCommand(PostInputDto Dto) : IRequest<PostDto>;

    public class CreatePostCommandHandler : BaseRequestHandler, IRequestHandler<CreatePostCommand, PostDto>
    {
        public CreatePostCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            return await CreateAsync<PostEntity, PostDto>(request.Dto);
        }
    }
}