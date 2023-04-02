using ApplicationCore.Bases;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Get;

public record GetRefreshTokenByUserIdQuery(Guid UserId) : IRequest<RefreshTokenDto>;

internal class GetRefreshTokenByUserIdQueryHandler : BaseRequestHandler<GetRefreshTokenByUserIdQuery, RefreshTokenDto>
{
    public GetRefreshTokenByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<RefreshTokenDto> Handle(GetRefreshTokenByUserIdQuery request, CancellationToken cancellationToken)
        => await GetElementAsync<RefreshTokenEntity, RefreshTokenDto>(x => x.UserId == request.UserId, true);
}