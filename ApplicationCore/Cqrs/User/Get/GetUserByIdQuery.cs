using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Get;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;

internal class GetUserByIdQueryHandler : BaseRequestHandler, IRequestHandler<GetUserByIdQuery, UserDto>
{
    public GetUserByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => GetElementAsync<UserEntity, UserDto>(x => x.Id.Equals(request.Id));
}