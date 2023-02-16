using ApplicationCore.Dtos.Comment;
using ApplicationCore.Dtos.Conversation;
using ApplicationCore.Dtos.ConversationMember;
using ApplicationCore.Dtos.Friend;
using ApplicationCore.Dtos.Login;
using ApplicationCore.Dtos.Message;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Dtos.User;
using AutoMapper;
using Domain.Entity;

namespace ApplicationCore.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        //Entity to Dto
        CreateMap<CommentEntity, CommentDto>();
        CreateMap<ConversationEntity, ConversationDto>();
        CreateMap<ConversationMemberEntity, ConversationMemberDto>();
        CreateMap<MessageEntity, MessageDto>();
        CreateMap<PostEntity, PostDto>();
        CreateMap<RefreshTokenEntity, RefreshTokenDto>();
        CreateMap<UserEntity, UserDto>();

        //Dto to Entity
        CreateMap<CommentInputDto, CommentEntity>();
        CreateMap<ConversationInputDto, ConversationEntity>();
        CreateMap<ConversationMemberInputDto, ConversationMemberEntity>();
        CreateMap<FriendInputDto, FriendEntity>();
        CreateMap<MessageInputDto, MessageEntity>();
        CreateMap<PostInputDto, PostEntity>();
        CreateMap<RefreshTokenInputDto, RefreshTokenEntity>();
        CreateMap<UserInputDto, UserEntity>();

        //Dto to Dto
        CreateMap<RegisterDto, UserInputDto>();
        CreateMap<UserDto, LoginDto>()
            .ForMember(x => x.Password, map => map.MapFrom(x => x.HashedPassword));
        CreateMap<UserDto, UserInputDto>().ReverseMap();
    }
}