using AutoMapper;
using MoviesApp.ViewModels;
namespace MoviesApp.Services.Actor.Dto.AutoMapperProfiles;

public class ActorDtoProfile:Profile
{
    public ActorDtoProfile()
    {
        CreateMap<Models.Actor, ActorDto>().ReverseMap();
    }
}