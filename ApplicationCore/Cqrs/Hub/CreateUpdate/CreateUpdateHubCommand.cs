using ApplicationCore.Bases;
using ApplicationCore.Dtos.Hub;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.Hub.CreateUpdate;
public record CreateUpdateHubCommand(HubInputDto Dto) : IRequest<HubDto>;

internal class CreateHubCommandHandler : BaseRequestHandler<CreateUpdateHubCommand, HubDto>
{
    public CreateHubCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<HubDto> Handle(CreateUpdateHubCommand request, CancellationToken cancellationToken)
    {
        var hub = await GetElementAsync<HubEntity, HubDto>(x => x.Type == request.Dto.Type && x.UserId == request.Dto.UserId);

        if (hub is null)
            return await CreateAsync<HubEntity, HubDto>(request.Dto);

        return await UpdateAsync<HubEntity, HubDto>(hub.Id, request.Dto);
    }
}