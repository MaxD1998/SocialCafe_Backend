using ApplicationCore.Bases;
using ApplicationCore.Dtos.Hub;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Hub.Create;
public record CreateHubCommand(HubInputDto Dto) : IRequest<HubDto>;

internal class CreateHubCommandHandler : BaseRequestHandler<CreateHubCommand, HubDto>
{
    public CreateHubCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<HubDto> Handle(CreateHubCommand request, CancellationToken cancellationToken)
        => await CreateAsync<HubEntity, HubDto>(request.Dto);
}