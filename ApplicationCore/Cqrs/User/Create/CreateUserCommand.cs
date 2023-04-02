using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.User.Create;

public record CreateUserCommand(UserInputDto Dto) : IRequest<UserDto>;

internal class CreateUserCommandHandler : BaseRequestHandler<CreateUserCommand, UserDto>
{
    public CreateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        => await CreateAsync<UserEntity, UserDto>(request.Dto);
}