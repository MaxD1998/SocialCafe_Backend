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
using ApplicationCore.Dtos.UserPhoto;
using AutoMapper;
using Domain.Entities;

namespace ApplicationCore.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        //Entity to Dto
        CreateMap<CommentEntity, CommentDto>();
        CreateMap<ConversationEntity, ConversationDto>();
        CreateMap<ConversationMemberEntity, ConversationMemberDto>();
        CreateMap<HubEntity, HubDto>();
        CreateMap<MessageEntity, MessageDto>();
        CreateMap<NotificationEntity, NotificationDto>();
        CreateMap<PostEntity, PostDto>();
        CreateMap<RefreshTokenEntity, RefreshTokenDto>();
        CreateMap<UserEntity, InviteUserDto>();
        CreateMap<UserEntity, UserDto>();
        CreateMap<UserPhotoEntity, UserPhotoDto>();
        CreateMap<UserPhotoEntity, UserPhotoListDto>();

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
        CreateMap<UserPhotoInputDto, UserPhotoEntity>();

        //Dto to Dto
        CreateMap<RegisterDto, UserInputDto>();
        CreateMap<UserDto, LoginDto>()
            .ForMember(x => x.Password, map => map.MapFrom(x => x.HashedPassword));
        CreateMap<UserDto, UserInputDto>().ReverseMap();
    }
}