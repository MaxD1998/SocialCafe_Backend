using ApplicationCore.Bases;
using ApplicationCore.Dtos.ConversationMember;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.ConversationMember.Create;

public record CreateConversationMemberCommand(ConversationMemberInputDto Dto) : IRequest<ConversationMemberDto>;

internal class CreateConversationMemberCommandHandler : BaseRequestHandler<CreateConversationMemberCommand, ConversationMemberDto>
{
    public CreateConversationMemberCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<ConversationMemberDto> Handle(CreateConversationMemberCommand request, CancellationToken cancellationToken)
        => await CreateAsync<ConversationMemberEntity, ConversationMemberDto>(request.Dto);
}