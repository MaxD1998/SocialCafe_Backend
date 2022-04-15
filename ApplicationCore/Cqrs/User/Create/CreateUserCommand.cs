using ApplicationCore.Bases;
using ApplicationCore.Dtos;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Create
{
    public record CreateUserCommand(UserDto Dto) : IRequest<UserDto>;

    public class CreateUserCommandHandler : BaseRequestHandler, IRequestHandler<CreateUserCommand, UserDto>
    {
        public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserEntity>(request.Dto);
            var result = await UnitOfWork.BaseRepository
                .CreateAsync(entity);

            return Mapper.Map<UserDto>(result);
        }
    }
}