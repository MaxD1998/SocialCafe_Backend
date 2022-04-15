using ApplicationCore.Bases;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Create
{
    public record CreateRefreshTokenCommand(int UserId, RefreshTokenEntity Entity) : IRequest<RefreshTokenEntity>;

    public class CreateRefreshTokenCommandHandler : BaseRequestHandler, IRequestHandler<CreateRefreshTokenCommand, RefreshTokenEntity>
    {
        public CreateRefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<RefreshTokenEntity> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await UnitOfWork.RefreshTokenRepository
                .CreateAsync(request.UserId, request.Entity);
        }
    }
}