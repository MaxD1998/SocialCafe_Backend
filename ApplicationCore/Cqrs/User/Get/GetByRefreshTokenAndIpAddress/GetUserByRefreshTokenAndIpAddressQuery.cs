using ApplicationCore.Bases;
using ApplicationCore.Dtos;
using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System.Net;

namespace ApplicationCore.Cqrs.User.Get.GetByRefreshTokenAndIpAddress
{
    public record GetUserByRefreshTokenAndIpAddressQuery(Guid RefreshToken, IPAddress IpAddress) : IRequest<UserDto>;

    public class GetUserByRefreshTokenAndIpAddressQueryHandler : BaseRequestHandler, IRequestHandler<GetUserByRefreshTokenAndIpAddressQuery, UserDto>
    {
        public GetUserByRefreshTokenAndIpAddressQueryHandler(IBaseRepository baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }

        public async Task<UserDto> Handle(GetUserByRefreshTokenAndIpAddressQuery request, CancellationToken cancellationToken)
        {
            var result = await BaseRepository
                .GetElement<UserEntity>(x => x.RefreshTokens
                    .Any(x => x.Token.Equals(request.RefreshToken)
                        && x.RemoteAddress.Equals(request.IpAddress)));

            return Mapper.Map<UserDto>(result);
        }
    }
}