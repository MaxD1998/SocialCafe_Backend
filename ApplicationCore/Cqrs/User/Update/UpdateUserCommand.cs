using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Cqrs.User.Update;

public record UpdateUserCommand(Guid Id, UserInputDto Dto) : IRequest<UserDto>;

internal class UpdateUserCommandHandler : BaseRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IPasswordHasher<UserInputDto> _passwordHasher;

    public UpdateUserCommandHandler(IPasswordHasher<UserInputDto> passwordHasher, IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
        _passwordHasher = passwordHasher;
    }

    public override async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Dto.Password != null)
            request.Dto.HashedPassword = _passwordHasher.HashPassword(request.Dto, request.Dto.Password);
        else
            request.Dto.HashedPassword = await GetElementAsync<UserEntity, string>(x => x.Id == request.Id, x => x.HashedPassword);

        return await UpdateAsync<UserEntity, UserDto>(request.Id, request.Dto);
    }
}