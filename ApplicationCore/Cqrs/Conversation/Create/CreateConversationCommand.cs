﻿using ApplicationCore.Bases;
using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Conversation.Create;

public record CreateConversationCommand(ConversationInputDto Dto) : IRequest<ConversationDto>;

internal class CreateConversationCommandHandler : BaseRequestHandler<CreateConversationCommand, ConversationDto>
{
    public CreateConversationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<ConversationDto> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
        => await CreateAsync<ConversationEntity, ConversationDto>(request.Dto);
}