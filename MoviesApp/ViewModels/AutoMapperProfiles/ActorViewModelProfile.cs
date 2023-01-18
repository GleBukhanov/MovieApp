using AutoMapper;
using MoviesApp.Models;

namespace MoviesApp.ViewModels.AutoMapperProfiles;

public class ActorViewModelProfile:Profile
{
    public ActorViewModelProfile()
    {
        CreateMap<Actor, InputActorViewModel>().ReverseMap();
        CreateMap<Actor, DeleteActorViewModel>();
        CreateMap<Actor, EditActorViewModel>().ReverseMap();
        CreateMap<Actor, ActorViewModel>();
    }
}