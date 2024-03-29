﻿using ApplicationCore.Bases;
using ApplicationCore.Dtos.Comment;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Comment.Create;

public record CreateCommentCommand(CommentInputDto Dto) : IRequest<CommentDto>;

internal class CreateCommentCommandHandler : BaseRequestHandler<CreateCommentCommand, CommentDto>
{
    public CreateCommentCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        => await CreateAsync<CommentEntity, CommentDto>(request.Dto);
}