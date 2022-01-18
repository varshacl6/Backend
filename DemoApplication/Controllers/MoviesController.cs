using DemoApplication.Models;
using DemoApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        // // GET: api/Movies
        // [HttpGet]
        // public IActionResult Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        // // GET: api/Movies/5
        // [HttpGet("{id}", Name = "Get")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // POST: api/Movies
        [HttpPost]
        public IActionResult Post([FromBody] MovieRequest movieRequest)
        {
            return Ok(_movieService.CreateMovie(movieRequest));
        }

        // // PUT: api/Movies/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }
        //
        // // DELETE: api/Movies/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
