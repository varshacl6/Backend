using System.Net;
using DemoApplication.Controllers;
using DemoApplication.Context;
using DemoApplication.Entities;
using DemoApplication.Models;
using DemoApplication.Repository;
using DemoApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DemoApplication.Tests.Controllers;

public class MoviesControllerTests
{
    private readonly MoviesController _moviesController;
    private readonly MovieRepository? _movieRepository;
    private readonly Mock<MovieService> _mockMovieService;
    
    public MoviesControllerTests()
    {
        _mockMovieService = new Mock<MovieService>(_movieRepository);    
        _moviesController = new MoviesController(_mockMovieService.Object);
    }
   
    [Fact( DisplayName = "Create a movie")]
    public void CreateMovieTest()
    {
        // Setup
        MovieRequest movieRequest = new MovieRequest(){Name = "Movie Name"};
        Movie savedMovie = new Movie() {Name = "Movie Name", Id = 1};
        _mockMovieService.Setup(movie => 
            movie.CreateMovie(movieRequest)
            ).Returns(savedMovie);
        
        // Execute
        ActionResult<Movie> result = _moviesController.Post(movieRequest);
        
        // Verify Assertions
        _mockMovieService.Verify((m => m.CreateMovie(movieRequest)), Times.Once());
        Assert.True(result.Result is OkObjectResult);
        Assert.Equal(savedMovie, ((OkObjectResult)result.Result)?.Value);
    }
}