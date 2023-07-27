using ApplicationCore.Bases;
using ApplicationCore.Dtos.UserPhoto;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace ApplicationCore.Cqrs.UserPhoto.Get;
public record GetMainUserPhotosByUserIdQuery(IEnumerable<Guid> UserIds) : IRequest<IEnumerable<UserPhotoListDto>>;

internal class GetMainUserPhotosByUserIdQueryHandler : BaseRequestHandler<GetMainUserPhotosByUserIdQuery, IEnumerable<UserPhotoListDto>>
{
    public GetMainUserPhotosByUserIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }

    public override async Task<IEnumerable<UserPhotoListDto>> Handle(GetMainUserPhotosByUserIdQuery request, CancellationToken cancellationToken)
        => await GetElementsAsync<UserPhotoEntity, UserPhotoListDto>(x => request.UserIds.Contains(x.UserId) && x.IsMain);
}