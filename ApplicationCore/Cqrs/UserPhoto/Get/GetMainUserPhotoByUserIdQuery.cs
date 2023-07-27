using ApplicationCore.Bases;
using ApplicationCore.Dtos.UserPhoto;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.UserPhoto.Get;
public record GetMainUserPhotoByUserIdQuery(Guid UserId) : IRequest<UserPhotoDto>;

internal class GetMainUserPhotoByUserIdQueryHandler : BaseRequestHandler<GetMainUserPhotoByUserIdQuery, UserPhotoDto>
{
    public GetMainUserPhotoByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<UserPhotoDto> Handle(GetMainUserPhotoByUserIdQuery request, CancellationToken cancellationToken)
        => await GetElementAsync<UserPhotoEntity, UserPhotoDto>(x => x.UserId == request.UserId && x.IsMain);
}