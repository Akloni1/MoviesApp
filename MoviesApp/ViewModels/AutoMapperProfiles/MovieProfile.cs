using MoviesApp.Models;
using AutoMapper;

namespace MoviesApp.ViewModels.AutoMapperProfiles
{
    public class MovieProfile: Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, InputMovieViewModel>().ReverseMap();
            CreateMap<Movie, DeleteMovieViewModel>();
            CreateMap<Movie, EditMovieViewModel>().ReverseMap();
            CreateMap<Movie, MovieViewModel>();
            CreateMap<Actor, InputActorViewModel>().ReverseMap();
            CreateMap<Actor, DeleteActorViewModel>();
            CreateMap<Actor, EditActorViewModel>().ReverseMap();
            CreateMap<Actor, ActorViewModel>();
        }
    }
}