using DemoApplication.Context;
using DemoApplication.Entities;

namespace DemoApplication.Repository;

public class MovieRepository
{
    private readonly ApplicationDbContext _dbContext;
    public MovieRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual Movie CreateMovie(Movie newMovie)
    {
        var savedMovieEntityEntry = _dbContext.Movies.Add(newMovie);
        _dbContext.SaveChanges();
        return savedMovieEntityEntry.Entity;
    }
}