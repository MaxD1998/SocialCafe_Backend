using ApplicationCore.Bases;
using ApplicationCore.Dtos.Message;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Message.Get;

public record GetMessagesByConversationIdQuery(int ConversationId) : IRequest<IEnumerable<MessageDto>>;

public class GetMessagesByConversationIdQueryHandler : BaseRequestHandler, IRequestHandler<GetMessagesByConversationIdQuery, IEnumerable<MessageDto>>
{
    public GetMessagesByConversationIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<IEnumerable<MessageDto>> Handle(GetMessagesByConversationIdQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<MessageEntity, MessageDto>(x => x.ConversationId.Equals(request.ConversationId));
}