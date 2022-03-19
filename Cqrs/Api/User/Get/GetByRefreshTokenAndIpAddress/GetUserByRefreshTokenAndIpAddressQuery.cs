using Domain.Entity;
using MediatR;
using System.Net;

namespace Cqrs.Api.User.Get.GetByRefreshTokenAndIpAddress
{
    public record GetUserByRefreshTokenAndIpAddressQuery(Guid RefreshToken, IPAddress IpAddress) : IRequest<UserEntity>;
}