using ApplicationCore.Bases;
using ApplicationCore.Dtos.ConversationMember;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.ConversationMember.Get;

public record GetConverastionMembersByConversationIdCommand(Guid ConversationId) : IRequest<IEnumerable<ConversationMemberDto>>;

internal class GetConverastionMembersByConversationIdCommandHandler
    : BaseRequestHandler
    , IRequestHandler<GetConverastionMembersByConversationIdCommand, IEnumerable<ConversationMemberDto>>
{
    public GetConverastionMembersByConversationIdCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public Task<IEnumerable<ConversationMemberDto>> Handle(GetConverastionMembersByConversationIdCommand request, CancellationToken cancellationToken)
        => GetElementsAsync<ConversationMemberEntity, ConversationMemberDto>(x => x.ConversationId.Equals(request.ConversationId));
}