﻿using ApplicationCore.Bases;
using ApplicationCore.Dtos.Friend;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Mappings.CustomMaps;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Friend.Create;

public record CreateFriendCommand(FriendInputDto Dto) : IRequest<FriendDto>;

internal class CreateFriendCommandHandler : BaseRequestHandler<CreateFriendCommand, FriendDto>
{
    public CreateFriendCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<FriendDto> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
    {
        Mapper = FriendMapProfile.Extend(request.Dto.InviterId);
        return await CreateAsync<FriendEntity, FriendDto>(request.Dto);
    }
}