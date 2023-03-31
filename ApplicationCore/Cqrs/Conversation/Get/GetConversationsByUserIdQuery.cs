﻿using ApplicationCore.Bases;
using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Conversation.Get;

public record GetConversationsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<ConversationDto>>;

internal class GetConversationsByUserIdQueryHandler : BaseRequestHandler, IRequestHandler<GetConversationsByUserIdQuery, IEnumerable<ConversationDto>>
{
    public GetConversationsByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public Task<IEnumerable<ConversationDto>> Handle(GetConversationsByUserIdQuery request, CancellationToken cancellationToken)
        => GetElementsAsync<ConversationEntity, ConversationDto>(x => x.ConversationMembers.Select(x => x.UserId).Contains(request.UserId));
}