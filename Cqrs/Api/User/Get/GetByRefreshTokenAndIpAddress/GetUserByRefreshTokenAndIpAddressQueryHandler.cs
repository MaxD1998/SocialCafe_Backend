using DataAccess;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cqrs.Api.User.Get.GetByRefreshTokenAndIpAddress
{
    public class GetUserByRefreshTokenAndIpAddressQueryHandler : IRequestHandler<GetUserByRefreshTokenAndIpAddressQuery, UserEntity>
    {
        public async Task<UserEntity> Handle(GetUserByRefreshTokenAndIpAddressQuery request, CancellationToken cancellationToken)
        {
            using (var context = new DataContext())
            {
                return await context.Users
                    .FirstOrDefaultAsync(x =>
                        x.RefreshTokens
                            .Any(x => x.Token.Equals(request.RefreshToken)
                                && x.RemoteAddress.Equals(request.IpAddress)));
            }
        }
    }
}