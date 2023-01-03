using ApplicationCore.Bases;
using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Conversation.Get;

public record GetConversationsByUserIdQuery(int UserId) : IRequest<IEnumerable<ConversationDto>>;

public class GetConversationsByUserIdQueryHandler : BaseRequestHandler, IRequestHandler<GetConversationsByUserIdQuery, IEnumerable<ConversationDto>>
{
    public GetConversationsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public Task<IEnumerable<ConversationDto>> Handle(GetConversationsByUserIdQuery request, CancellationToken cancellationToken)
        => GetElementsAsync<ConversationEntity, ConversationDto>(x => x.ConversationMembers.Select(x => x.UserId).Contains(request.UserId));
}