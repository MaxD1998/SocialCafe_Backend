using ApplicationCore.Bases;
using ApplicationCore.Dtos.Comment;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Comment.Get;

public record GetCommentsByPostIdQuery(Guid PostId) : IRequest<IEnumerable<CommentDto>>;

internal class GetCommentsByPostIdQueryHandler : BaseRequestHandler<GetCommentsByPostIdQuery, IEnumerable<CommentDto>>
{
    public GetCommentsByPostIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<CommentDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<CommentEntity, CommentDto>(x => x.PostId == request.PostId);
}