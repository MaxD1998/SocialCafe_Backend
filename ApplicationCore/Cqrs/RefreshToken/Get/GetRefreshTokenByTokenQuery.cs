using ApplicationCore.Bases;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Get;

public record GetRefereshTokenByTokenQuery(Guid Token) : IRequest<RefreshTokenDto>;

internal class GetRefreshTokenByTokenQueryHandle : BaseRequestHandler<GetRefereshTokenByTokenQuery, RefreshTokenDto>
{
    public GetRefreshTokenByTokenQueryHandle(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<RefreshTokenDto> Handle(GetRefereshTokenByTokenQuery request, CancellationToken cancellationToken)
        => await GetElementAsync<RefreshTokenEntity, RefreshTokenDto>(x => x.Token.Equals(request.Token));
}