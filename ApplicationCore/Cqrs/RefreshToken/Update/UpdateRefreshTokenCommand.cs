using ApplicationCore.Bases;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Update
{
    public record UpdateRefreshTokenCommand(int UserId, RefreshTokenEntity Entity) : IRequest<RefreshTokenEntity>;

    public class UpdateRefreshTokenCommandHandler : BaseRequestHandler, IRequestHandler<UpdateRefreshTokenCommand, RefreshTokenEntity>
    {
        public UpdateRefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<RefreshTokenEntity> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await UnitOfWork.RefreshTokenRepository
                .UpdateAsync(request.UserId, request.Entity);
        }
    }
}