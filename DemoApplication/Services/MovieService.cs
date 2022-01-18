using DemoApplication.Data;
using DemoApplication.Entities;
using DemoApplication.Models;

namespace DemoApplication.Services;

public class MovieService
{
    private readonly ApplicationDbContext _dbContext;
    public MovieService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Movie CreateMovie(MovieRequest movieRequest)
    {
        Movie newMovie = new Movie()
        {
            Name = movieRequest.Name
        };
        var savedMovieEntityEntry = _dbContext.Movies.Add(newMovie);
        _dbContext.SaveChanges();
        return savedMovieEntityEntry.Entity;
    }
}