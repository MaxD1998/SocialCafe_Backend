using ApplicationCore.Bases;
using ApplicationCore.Dtos;
using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Create
{
    public record CreateUserCommand(UserDto Dto) : IRequest<UserDto>;

    public class CreateUserCommandHandler : BaseRequestHandler, IRequestHandler<CreateUserCommand, UserDto>
    {
        public CreateUserCommandHandler(IBaseRepository baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<UserEntity>(request.Dto);
            var result = await BaseRepository.CreateAsync(entity);

            return Mapper.Map<UserDto>(result);
        }
    }
}