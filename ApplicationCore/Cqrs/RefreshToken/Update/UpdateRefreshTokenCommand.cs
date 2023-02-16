using ApplicationCore.Bases;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entity;
using MediatR;

namespace ApplicationCore.Cqrs.RefreshToken.Update;

public record UpdateRefreshTokenCommand(int Id, RefreshTokenInputDto Dto) : IRequest<RefreshTokenDto>;

public class UpdateRefreshTokenCommandHandler : BaseRequestHandler, IRequestHandler<UpdateRefreshTokenCommand, RefreshTokenDto>
{
    public UpdateRefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public async Task<RefreshTokenDto> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
        => await UpdateAsync<RefreshTokenEntity, RefreshTokenDto>(request.Id, request.Dto);
}