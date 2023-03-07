using ApplicationCore.Bases;
using ApplicationCore.Dtos.ConversationMember;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.ConversationMember.Create;

public record CreateConversationMemberCommand(ConversationMemberInputDto Dto) : IRequest<ConversationMemberDto>;

internal class CreateConversationMemberCommandHandler : BaseRequestHandler, IRequestHandler<CreateConversationMemberCommand, ConversationMemberDto>
{
    public CreateConversationMemberCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public Task<ConversationMemberDto> Handle(CreateConversationMemberCommand request, CancellationToken cancellationToken)
        => CreateAsync<ConversationMemberEntity, ConversationMemberDto>(request.Dto);
}