using ApplicationCore.Bases;
using ApplicationCore.Dtos.Message;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Message.Get;

public record GetMessagesByConversationIdQuery(Guid ConversationId) : IRequest<IEnumerable<MessageDto>>;

internal class GetMessagesByConversationIdQueryHandler : BaseRequestHandler<GetMessagesByConversationIdQuery, IEnumerable<MessageDto>>
{
    public GetMessagesByConversationIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<MessageDto>> Handle(GetMessagesByConversationIdQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<MessageEntity, MessageDto>(x => x.ConversationId.Equals(request.ConversationId));
}