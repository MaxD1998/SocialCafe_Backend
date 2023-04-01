using ApplicationCore.Bases;
using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Conversation.Get;

public record GetConversationsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<ConversationDto>>;

internal class GetConversationsByUserIdQueryHandler : BaseRequestHandler<GetConversationsByUserIdQuery, IEnumerable<ConversationDto>>
{
    public GetConversationsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<ConversationDto>> Handle(GetConversationsByUserIdQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<ConversationEntity, ConversationDto>(x => x.ConversationMembers.Select(x => x.UserId).Contains(request.UserId));
}