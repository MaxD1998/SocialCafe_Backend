using ApplicationCore.Bases;
using ApplicationCore.Dtos.Comment;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Comment.Get
{
    public record GetCommentsByPostIdQuery(int PostId) : IRequest<IEnumerable<CommentDto>>;

    public class GetCommentsByPostIdQueryHandler : BaseRequestHandler, IRequestHandler<GetCommentsByPostIdQuery, IEnumerable<CommentDto>>
    {
        public GetCommentsByPostIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<IEnumerable<CommentDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
            => await GetElementsAsync<CommentEntity, CommentDto>(x => x.PostId.Equals(request.PostId));
    }
}