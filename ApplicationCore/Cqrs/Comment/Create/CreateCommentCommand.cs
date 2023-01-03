using ApplicationCore.Bases;
using ApplicationCore.Dtos.Comment;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Comment.Create;

public record CreateCommentCommand(CommentInputDto Dto) : IRequest<CommentDto>;

public class CreateCommentCommandHandler : BaseRequestHandler, IRequestHandler<CreateCommentCommand, CommentDto>
{
    public CreateCommentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        => await CreateAsync<CommentEntity, CommentDto>(request.Dto);
}