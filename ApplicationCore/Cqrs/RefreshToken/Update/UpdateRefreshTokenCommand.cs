using ApplicationCore.Bases;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Update;

public record UpdateRefreshTokenCommand(Guid Id, RefreshTokenInputDto Dto) : IRequest<RefreshTokenDto>;

internal class UpdateRefreshTokenCommandHandler : BaseRequestHandler<UpdateRefreshTokenCommand, RefreshTokenDto>
{
    public UpdateRefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<RefreshTokenDto> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        => await UpdateAsync<RefreshTokenEntity, RefreshTokenDto>(request.Id, request.Dto);
}