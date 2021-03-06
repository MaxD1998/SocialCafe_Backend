using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Create
{
    public record CreateUserCommand(UserInputDto Dto) : IRequest<UserDto>;

    public class CreateUserCommandHandler : BaseRequestHandler, IRequestHandler<CreateUserCommand, UserDto>
    {
        public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await CreateAsync<UserEntity, UserDto>(request.Dto);
        }
    }
}