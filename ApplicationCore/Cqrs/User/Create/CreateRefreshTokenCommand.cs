using ApplicationCore.Bases;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Create
{
    public record CreateRefreshTokenCommand(int UserId, RefreshTokenInputDto Dto) : IRequest<UserDto>;

    public class CreateRefreshTokenCommandHandler : BaseRequestHandler, IRequestHandler<CreateRefreshTokenCommand, UserDto>
    {
        public CreateRefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<UserDto> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<RefreshTokenEntity>(request.Dto);
            var result = await UnitOfWork.UserRepository
                .CreateRefreshTokenAsync(request.UserId, entity);

            return Mapper.Map<UserDto>(result);
        }
    }
}