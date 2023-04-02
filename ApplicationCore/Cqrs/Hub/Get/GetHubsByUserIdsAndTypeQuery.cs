using ApplicationCore.Bases;
using ApplicationCore.Dtos.Hub;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace ApplicationCore.Cqrs.Hub.Get;
public record GetHubsByUserIdsAndTypeQuery(IEnumerable<Guid> UserIds, HubType Type) : IRequest<IEnumerable<HubDto>>;

internal class GetHubsByUserIdsAndTypeQueryHandler : BaseRequestHandler<GetHubsByUserIdsAndTypeQuery, IEnumerable<HubDto>>
{
    public GetHubsByUserIdsAndTypeQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<HubDto>> Handle(GetHubsByUserIdsAndTypeQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<HubEntity, HubDto>(x => request.UserIds.Contains(x.UserId) && x.Type == request.Type);
}