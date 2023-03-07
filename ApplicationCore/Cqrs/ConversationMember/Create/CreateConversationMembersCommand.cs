using ApplicationCore.Bases;
using ApplicationCore.Dtos.ConversationMember;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.ConversationMember.Create;

public record CreateConversationMembersCommand(IEnumerable<ConversationMemberInputDto> Dtos) : IRequest<IEnumerable<ConversationMemberDto>>;

internal class CreateConversationMembersCommandHandler : BaseRequestHandler, IRequestHandler<CreateConversationMembersCommand, IEnumerable<ConversationMemberDto>>
{
    public CreateConversationMembersCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<IEnumerable<ConversationMemberDto>> Handle(CreateConversationMembersCommand request, CancellationToken cancellationToken)
        => await CreateRangeAsync<ConversationMemberEntity, ConversationMemberDto>(request.Dtos);
}