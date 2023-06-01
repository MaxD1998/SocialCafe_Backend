using ApplicationCore.Bases;
using ApplicationCore.Dtos.Friend;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Mappings.CustomMaps;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Friend.Get;

public record GetFriendsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<FriendDto>>;

internal class GetFriendsByUserIdQueryHandler : BaseRequestHandler<GetFriendsByUserIdQuery, IEnumerable<FriendDto>>
{
    public GetFriendsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<FriendDto>> Handle(GetFriendsByUserIdQuery request, CancellationToken cancellationToken)
    {
        Mapper = FriendMapProfile.Extend(request.UserId);
        return await GetElementsAsync<FriendEntity, FriendDto>(x => x.InviterId == request.UserId || x.RecipientId == request.UserId);
    }
}