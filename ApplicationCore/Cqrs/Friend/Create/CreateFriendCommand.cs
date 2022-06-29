using ApplicationCore.Bases;
using ApplicationCore.Dtos.Friend;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Friend.Create
{
    public record CreateFriendCommand(FriendInputDto Dto) : IRequest<FriendDto>;

    public class CreateFriendCommandHandler : BaseRequestHandler, IRequestHandler<CreateFriendCommand, FriendDto>
    {
        public CreateFriendCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<FriendDto> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
        {
            return await CreateAsync<FriendEntity, FriendDto>(request.Dto);
        }
    }
}