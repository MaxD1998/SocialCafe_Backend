using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Update;

public record UpdateUserCommand(Guid Id, UserInputDto Dto) : IRequest<UserDto>;

internal class UpdateUserCommandHandler : BaseRequestHandler, IRequestHandler<UpdateUserCommand, UserDto>
{
    public UpdateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        => UpdateAsync<UserEntity, UserDto>(request.Id, request.Dto);
}