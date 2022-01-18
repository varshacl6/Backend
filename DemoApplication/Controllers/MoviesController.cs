using DemoApplication.Models;
using DemoApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DemoApplication.Entities;

namespace DemoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public  MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        // POST: api/Movies
        [HttpPost]
        public ActionResult<Movie> Post([FromBody] MovieRequest movieRequest)
        {
            var result= _movieService.CreateMovie(movieRequest);
            return Ok(result);
        }
    }
}
