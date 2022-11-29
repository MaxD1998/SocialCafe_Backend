using ApplicationCore.Bases;
using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Conversation.Create
{
    public record CreateConversationCommand(ConversationInputDto Dto) : IRequest<ConversationDto>;

    public class CreateConversationCommandHandler : BaseRequestHandler, IRequestHandler<CreateConversationCommand, ConversationDto>
    {
        public CreateConversationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public Task<ConversationDto> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
            => CreateAsync<ConversationEntity, ConversationDto>(request.Dto);
    }
}