using ApplicationCore.Bases;
using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Conversation.Get;

public record GetConversationByIdQuery(Guid Id) : IRequest<ConversationDto>;

internal class GetConversationByIdQueryHandler : BaseRequestHandler<GetConversationByIdQuery, ConversationDto>
{
    public GetConversationByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<ConversationDto> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
        => await GetElementAsync<ConversationEntity, ConversationDto>(x => x.Id == request.Id);
}