using ApplicationCore.Bases;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Update
{
    public record UpdateRefreshTokenCommand(int UserId, int RefreshTokenId, RefreshTokenInputDto Dto) : IRequest<UserDto>;

    public class UpdateRefreshTokenCommandHandler : BaseRequestHandler, IRequestHandler<UpdateRefreshTokenCommand, UserDto>
    {
        public UpdateRefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<UserDto> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<RefreshTokenEntity>(request.Dto);
            var result = await UnitOfWork.UserRepository
                .UpdateRefreshTokenAsync(request.UserId, request.RefreshTokenId, entity);

            return Mapper.Map<UserDto>(result);
        }
    }
}