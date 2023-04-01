using ApplicationCore.Bases;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Create;

public record CreateRefreshTokenCommand(RefreshTokenInputDto Dto) : IRequest<RefreshTokenDto>;

internal class CreateRefreshTokenCommandHandler : BaseRequestHandler<CreateRefreshTokenCommand, RefreshTokenDto>
{
    public CreateRefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<RefreshTokenDto> Handle(CreateRefreshTokenCommand request, CancellationToken cancellationToken)
        => await CreateAsync<RefreshTokenEntity, RefreshTokenDto>(request.Dto);
}