using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Get
{
    public record GetUsersQuery() : IRequest<IEnumerable<UserDto>>;

    public class GetUsersQueryHandler : BaseRequestHandler, IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        public GetUsersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            return await GetAllAsync<UserEntity, UserDto>();
        }
    }
}