using Api.Models;
using AutoMapper;

namespace Api.Mapper
{
    public class DtoToDomain : Profile
    {
        public DtoToDomain()
        {
            CreateMap<RegisterDto, DtoToDomain>();
        }
    }
}