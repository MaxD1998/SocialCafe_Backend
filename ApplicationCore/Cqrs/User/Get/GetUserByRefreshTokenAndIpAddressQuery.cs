using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System.Net;

namespace ApplicationCore.Cqrs.User.Get
{
    public record GetUserByRefreshTokenAndIpAddressQuery(Guid RefreshToken, IPAddress IpAddress) : IRequest<UserDto>;

    public class GetUserByRefreshTokenAndIpAddressQueryHandler : BaseRequestHandler, IRequestHandler<GetUserByRefreshTokenAndIpAddressQuery, UserDto>
    {
        public GetUserByRefreshTokenAndIpAddressQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<UserDto> Handle(GetUserByRefreshTokenAndIpAddressQuery request, CancellationToken cancellationToken)
        {
            return await GetElementAsync<UserEntity, UserDto>(x => x.RefreshTokens
                    .Any(x => x.Token.Equals(request.RefreshToken)
                        && x.RemoteAddress.Equals(request.IpAddress)));
        }
    }
}