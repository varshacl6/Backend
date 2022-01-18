using DemoApplication.Context;
using DemoApplication.Entities;
using DemoApplication.Models;
using DemoApplication.Repository;

namespace DemoApplication.Services;

public class MovieService
{
    private readonly MovieRepository _movieRepository;
    public MovieService(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    // this method is made virtual so that the _mockMovieService.Setup in Controller tests could override when mocking this function result
    public virtual Movie CreateMovie(MovieRequest movieRequest)
    {
        Movie newMovie = new Movie() {Name = movieRequest.Name};
        Movie savedMovie = _movieRepository.CreateMovie(newMovie);
        return savedMovie;
    }
}