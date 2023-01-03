using ApplicationCore.Bases;
using ApplicationCore.Dtos.Message;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.Message.Get;

public record GetMessagesQuery() : IRequest<IEnumerable<MessageDto>>;

public class GetMessagesQueryHandler : BaseRequestHandler, IRequestHandler<GetMessagesQuery, IEnumerable<MessageDto>>
{
    public GetMessagesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<IEnumerable<MessageDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        => await GetAllAsync<MessageEntity, MessageDto>();
}