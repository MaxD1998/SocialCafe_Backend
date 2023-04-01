using ApplicationCore.Bases;
using ApplicationCore.Dtos.Message;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Message.Create;

public record CreateMessageCommand(MessageInputDto Dto) : IRequest<MessageDto>;

internal class CreateMessageCommandHandler : BaseRequestHandler<CreateMessageCommand, MessageDto>
{
    public CreateMessageCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        => await CreateAsync<MessageEntity, MessageDto>(request.Dto);
}