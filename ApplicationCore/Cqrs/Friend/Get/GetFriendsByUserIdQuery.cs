using ApplicationCore.Bases;
using ApplicationCore.Dtos.Friend;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Friend.Get
{
    public record GetFriendsByUserIdQuery(int UserId) : IRequest<IEnumerable<FriendDto>>;

    public class GetFriendsByUserIdQueryHandler : BaseRequestHandler, IRequestHandler<GetFriendsByUserIdQuery, IEnumerable<FriendDto>>
    {
        public GetFriendsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<IEnumerable<FriendDto>> Handle(GetFriendsByUserIdQuery request, CancellationToken cancellationToken)
            => await GetElementsAsync<FriendEntity, FriendDto>(x => x.InviterId == request.UserId || x.RecipientId == request.UserId);
    }
}