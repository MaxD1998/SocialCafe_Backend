﻿using Api.Bases;
using ApplicationCore.Cqrs.Friend.Create;
using ApplicationCore.Cqrs.Friend.Delete;
using ApplicationCore.Cqrs.Friend.Get;
using ApplicationCore.Dtos.Friend;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class FriendController : BaseApiController
{
    public FriendController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<ActionResult<FriendDto>> CreateAsync([FromBody] FriendInputDto dto)
        => await ApiResponseAsync<FriendDto, CreateFriendCommand>(new(dto));

    [HttpDelete("UserFriend/{id}")]
    public async Task<ActionResult<bool>> DeleteUserFriendAsync([FromRoute] Guid id)
        => await ApiResponseAsync<bool, DeleteUserFriendCommand>(new(id));

    [HttpGet("UserId/{userId}")]
    public async Task<ActionResult<IEnumerable<FriendDto>>> GetsByUserIdAsync([FromRoute] Guid userId)
        => await ApiResponseAsync<IEnumerable<FriendDto>, GetFriendsByUserIdQuery>(new(userId));
}