using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Get;

public record GetUsersByFirstNameAndLastNameQuery(string FirstName, string LastName) : IRequest<IEnumerable<UserDto>>;

public class GetUsersByFirstNameAndLastNameQueryHandler : BaseRequestHandler, IRequestHandler<GetUsersByFirstNameAndLastNameQuery, IEnumerable<UserDto>>
{
    public GetUsersByFirstNameAndLastNameQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<IEnumerable<UserDto>> Handle(GetUsersByFirstNameAndLastNameQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<UserEntity, UserDto>(x => x.FirstName.ToLower() == request.FirstName.ToLower()
            && x.LastName.ToLower() == request.LastName.ToLower());
}