﻿using ApplicationCore.Bases;
using ApplicationCore.Dtos.User;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.User.Get;

public record GetUsersQuery() : IRequest<IEnumerable<UserDto>>;

internal class GetUsersQueryHandler : BaseRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public GetUsersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        => await GetAllAsync<UserEntity, UserDto>();
}