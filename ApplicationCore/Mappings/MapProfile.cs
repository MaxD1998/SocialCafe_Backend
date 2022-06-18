using ApplicationCore.Dtos.Comment;
using ApplicationCore.Dtos.Login;
using ApplicationCore.Dtos.Post;
using ApplicationCore.Dtos.RefreshToken;
using ApplicationCore.Dtos.User;
using AutoMapper;
using Domain.Entity;

namespace ApplicationCore.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CommentDto, CommentEntity>().ReverseMap();
            CreateMap<CommentInputDto, CommentEntity>().ReverseMap();
            CreateMap<PostDto, PostEntity>().ReverseMap();
            CreateMap<PostInputDto, PostEntity>().ReverseMap();
            CreateMap<RefreshTokenDto, RefreshTokenEntity>().ReverseMap();
            CreateMap<RefreshTokenInputDto, RefreshTokenEntity>().ReverseMap();
            CreateMap<RegisterDto, UserInputDto>().ReverseMap();
            CreateMap<UserDto, LoginDto>()
                .ForMember(x => x.Password, map => map.MapFrom(x => x.HashedPassword));
            CreateMap<UserDto, UserEntity>().ReverseMap();
            CreateMap<UserDto, UserInputDto>().ReverseMap();
        }
    }
}