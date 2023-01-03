using ApplicationCore.Bases;
using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Conversation.Get;

public record GetConversationByIdQuery(int Id) : IRequest<ConversationDto>;

public class GetConversationByIdQueryHandler : BaseRequestHandler, IRequestHandler<GetConversationByIdQuery, ConversationDto>
{
    public GetConversationByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<ConversationDto> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
        => await GetElementAsync<ConversationEntity, ConversationDto>(x => x.Id.Equals(request.Id));
}