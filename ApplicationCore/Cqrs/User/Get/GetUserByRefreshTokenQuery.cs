using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Get
{
    public record GetUserByRefreshTokenQuery(Guid RefreshToken) : IRequest<UserDto>;

    public class GetUserByRefreshTokenQueryHandler : BaseRequestHandler, IRequestHandler<GetUserByRefreshTokenQuery, UserDto>
    {
        public GetUserByRefreshTokenQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<UserDto> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            return await GetElementAsync<UserEntity, UserDto>(x => x.RefreshTokens
                    .Any(x => x.Token.Equals(request.RefreshToken)));
        }
    }
}