using ApplicationCore.Bases;
using ApplicationCore.Dtos;
using ApplicationCore.Interfaces;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.User.Get.GetByEmail
{
    public record GetUserByEmailQuery(string Email) : IRequest<UserDto>;

    public class GetUserByEmailQueryHandler : BaseRequestHandler, IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        public GetUserByEmailQueryHandler(IBaseRepository baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await BaseRepository
                .GetElement<UserEntity>(x => x.Email.Equals(request.Email));

            return Mapper.Map<UserDto>(result);
        }
    }
}