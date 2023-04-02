using ApplicationCore.Dtos.Comment;
using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Dtos.ConversationMember;
using ApplicationCore.Dtos.Friend;
using ApplicationCore.Dtos.Hub;
using ApplicationCore.Dtos.Login;
using ApplicationCore.Dtos.Message;
using ApplicationCore.Dtos.Notification;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Dtos.User;
using ApplicationCore.Extensions;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Mappings;

public class MapProfile : Profile
{
    private readonly Guid _userId;

    public MapProfile(IHttpContextAccessor httpContextAccessor)
    {
        _userId = InitUserId(httpContextAccessor);

        //Entity to Dto
        CreateMap<CommentEntity, CommentDto>();
        CreateMap<ConversationEntity, ConversationDto>();
        CreateMap<ConversationMemberEntity, ConversationMemberDto>();
        CreateMap<FriendEntity, FriendDto>()
            .ForMember(x => x.UserId, map => map.MapFrom(x => x.InviterId != _userId ? x.InviterId : x.RecipientId))
            .ForMember(x => x.User, map => map.MapFrom(x => x.InviterId != _userId ? x.Inviter : x.Recipient));
        CreateMap<HubEntity, HubDto>();
        CreateMap<MessageEntity, MessageDto>();
        CreateMap<NotificationEntity, NotificationDto>();
        CreateMap<PostEntity, PostDto>();
        CreateMap<RefreshTokenEntity, RefreshTokenDto>();
        CreateMap<UserEntity, UserDto>();

        //Dto to Entity
        CreateMap<CommentInputDto, CommentEntity>();
        CreateMap<ConversationInputDto, ConversationEntity>();
        CreateMap<ConversationMemberInputDto, ConversationMemberEntity>();
        CreateMap<FriendInputDto, FriendEntity>();
        CreateMap<HubInputDto, HubEntity>();
        CreateMap<MessageInputDto, MessageEntity>();
        CreateMap<NotificationInputDto, NotificationEntity>();
        CreateMap<PostInputDto, PostEntity>();
        CreateMap<RefreshTokenInputDto, RefreshTokenEntity>();
        CreateMap<UserInputDto, UserEntity>();

        //Dto to Dto
        CreateMap<RegisterDto, UserInputDto>();
        CreateMap<UserDto, LoginDto>()
            .ForMember(x => x.Password, map => map.MapFrom(x => x.HashedPassword));
        CreateMap<UserDto, UserInputDto>().ReverseMap();
    }

    private Guid InitUserId(IHttpContextAccessor httpContextAccessor)
            => httpContextAccessor.HttpContext.User.GetUserId();
}