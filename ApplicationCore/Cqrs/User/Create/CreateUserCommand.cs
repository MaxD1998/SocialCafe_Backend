using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Cqrs.User.Create;

public record CreateUserCommand(UserInputDto Dto) : IRequest<UserDto>;

internal class CreateUserCommandHandler : BaseRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IPasswordHasher<UserInputDto> _passwordHasher;

    public CreateUserCommandHandler(IPasswordHasher<UserInputDto> passwordHasher, IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _passwordHasher = passwordHasher;
    }

    public override async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        request.Dto.HashedPassword = _passwordHasher.HashPassword(request.Dto, request.Dto.Password);
        return await CreateAsync<UserEntity, UserDto>(request.Dto);
    }
}