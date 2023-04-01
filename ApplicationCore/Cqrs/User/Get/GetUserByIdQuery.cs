using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.User.Get;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;

internal class GetUserByIdQueryHandler : BaseRequestHandler<GetUserByIdQuery, UserDto>
{
    public GetUserByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        => await GetElementAsync<UserEntity, UserDto>(x => x.Id.Equals(request.Id));
}