using ApplicationCore.Bases;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Delete
{
    public record DeleteRefreshTokensCommand(int UserId, List<RefreshTokenDto> RefreshTokens) : IRequest<bool>;

    public class DeleteRefreshTokensCommandHandler : BaseRequestHandler, IRequestHandler<DeleteRefreshTokensCommand, bool>
    {
        public DeleteRefreshTokensCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<bool> Handle(DeleteRefreshTokensCommand request, CancellationToken cancellationToken)
        {
            var entities = Mapper.Map<IEnumerable<RefreshTokenEntity>>(request.RefreshTokens);
            return await UnitOfWork.UserRepository.DeleteRefreshTokensAsync(request.UserId, entities);
        }
    }
}