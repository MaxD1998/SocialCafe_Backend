using ApplicationCore.Cqrs.Friend.Get;
using ApplicationCore.Cqrs.User.Get;
using ApplicationCore.Cqrs.User.Update;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces;
using AutoMapper;
using MediatR;

namespace ApplicationCore.Sevices;

public class ChatService : IChatService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ChatService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<IEnumerable<string>> GetConnectionIds(int userId)
    {
        var friends = await _mediator.Send(new GetFriendsByUserIdQuery(userId));
        return friends.Select(x => x.User.ConnectionId);
    }

    public async Task UpdateUserConnectionId(int userId, string connectionId)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(userId));
        var userInput = _mapper.Map<UserInputDto>(user);

        userInput.ConnectionId = connectionId;
        await _mediator.Send(new UpdateUserCommand(userId, userInput));
    }
}