using DataAccess;
using Domain.Entity;
using MediatR;

namespace Cqrs.Api.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity>
    {
        public async Task<UserEntity> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using (var context = new DataContext())
            {
                var result = await context.Users.AddAsync(request.Entity);

                await context.SaveChangesAsync();

                return result.Entity;
            }
        }
    }
}