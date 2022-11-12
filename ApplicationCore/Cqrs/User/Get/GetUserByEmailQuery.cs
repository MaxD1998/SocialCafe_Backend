using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Get
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;

    public class GetUserByEmailQueryHandler : BaseRequestHandler, IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        public GetUserByEmailQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
            => await GetElementAsync<UserEntity, UserDto>(x => x.Email.ToLower() == request.Email.ToLower());
    }
}