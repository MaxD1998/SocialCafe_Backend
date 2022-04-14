using Common.Constants;
using Common.Exceptions;
using Common.Extensions;
using DataAccess;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Cqrs.RefreshToken.Update
{
    public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand, RefreshTokenEntity>
    {
        public async Task<RefreshTokenEntity> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            using (var context = new DataContext())
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(x => x.Id.Equals(request.UserId));

                user.ThrowIfNull(new NotFoundException(ExceptionMessageConst.RefreshTokenUpdateFailed));

                var refreshToken = user.RefreshTokens
                    .FirstOrDefault(x => x.RemoteAddress.Equals(request.Entity.RemoteAddress));

                refreshToken.ThrowIfNull(new NotFoundException(ExceptionMessageConst.RefreshTokenUpdateFailed));

                Map(request.Entity, refreshToken);
                await context.SaveChangesAsync();

                return refreshToken;
            }
        }

        private void Map(RefreshTokenEntity source, RefreshTokenEntity destination)
        {
            destination.CreationDate = source.CreationDate;
            destination.ExpireDate = source.ExpireDate;
            destination.Token = source.Token;
        }
    }
}