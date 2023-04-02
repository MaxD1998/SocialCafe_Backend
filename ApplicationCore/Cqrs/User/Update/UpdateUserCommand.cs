using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.User.Update;

public record UpdateUserCommand(Guid Id, UserInputDto Dto) : IRequest<UserDto>;

internal class UpdateUserCommandHandler : BaseRequestHandler<UpdateUserCommand, UserDto>
{
    public UpdateUserCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        => await UpdateAsync<UserEntity, UserDto>(request.Id, request.Dto);
}