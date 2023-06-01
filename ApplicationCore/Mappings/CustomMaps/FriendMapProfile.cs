using ApplicationCore.Dtos.Friend;
using AutoMapper;
using Domain.Entities;

namespace ApplicationCore.Mappings.CustomMaps;

public static class FriendMapProfile
{
    public static IMapper Extend(Guid userId)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapProfile());
            cfg.CreateMap<FriendEntity, FriendDto>()
                .ForMember(dest => dest.UserId, map => map.MapFrom(src => src.InviterId != userId ? src.InviterId : src.RecipientId))
                .ForMember(dest => dest.User, map => map.MapFrom(src => src.InviterId != userId ? src.Inviter : src.Recipient));
        });

        return config.CreateMapper();
    }
}