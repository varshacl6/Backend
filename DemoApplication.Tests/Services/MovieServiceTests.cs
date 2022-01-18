using DemoApplication.Context;
using DemoApplication.Entities;
using DemoApplication.Models;
using DemoApplication.Repository;
using DemoApplication.Services;
using Moq;
using Xunit;

namespace DemoApplication.Tests.Services;

public class MovieServiceTests
{
    private readonly MovieService _movieService;
    private readonly Mock<MovieRepository> _mockMovieRepository;
    private readonly IApplicationDbContext _dbContext;
    
    public MovieServiceTests()
    {
        _mockMovieRepository = new Mock<MovieRepository>(_dbContext);
        _movieService = new MovieService(_mockMovieRepository.Object);
    }
    
    
    [Fact( DisplayName = "Create a movie")]
    public void CreateMovieServiceMethodTest()
    {
        // Setup
        MovieRequest movieRequest = new MovieRequest(){Name = "Movie Name"};
        Movie nonSavedMovie = new Movie() {Name = "Movie Name"};
        Movie savedMovie = new Movie() {Name = "Movie Name", Id = 1};
        _mockMovieRepository.Setup(movieRepository => 
            movieRepository.CreateMovie( It.Is<Movie>(m => m.Name==movieRequest.Name))
        ).Returns(savedMovie);
        
        // Execute
       Movie result = _movieService.CreateMovie(movieRequest);
        
        // Verify Assertions
        _mockMovieRepository.Verify((m => m.CreateMovie(It.Is<Movie>(m => m.Name==movieRequest.Name))), Times.Once());

        Assert.Equal(savedMovie, result);
    }
}