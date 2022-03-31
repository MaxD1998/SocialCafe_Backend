using DataAccess;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cqrs.Api.User.Get.GetByEmail
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserEntity>
    {
        public async Task<UserEntity> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            using (var context = new DataContext())
            {
                return await context.Users
                    .FirstOrDefaultAsync(x => x.Email.Equals(request.Email));
            }
        }
    }
}