using ApplicationCore.Bases;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Get;

public record GetRefreshTokenByUserIdQuery(int UserId) : IRequest<RefreshTokenDto>;

public class GetRefreshTokenByUserIdQueryHandler : BaseRequestHandler, IRequestHandler<GetRefreshTokenByUserIdQuery, RefreshTokenDto>
{
    public GetRefreshTokenByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public Task<RefreshTokenDto> Handle(GetRefreshTokenByUserIdQuery request, CancellationToken cancellationToken)
        => GetElementAsync<RefreshTokenEntity, RefreshTokenDto>(x => x.UserId.Equals(request.UserId), true);
}