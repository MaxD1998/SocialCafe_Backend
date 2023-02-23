using ApplicationCore.Mappings;
using AutoMapper;

namespace Api.Extensions;

public static class AutoMapperExtension
{
    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddScoped(x => new MapperConfiguration(config =>
        {
            config.AddProfile(new MapProfile(x.GetService<IHttpContextAccessor>()));
        })
        .CreateMapper());
    }
}