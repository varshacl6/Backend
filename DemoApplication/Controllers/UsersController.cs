using System.Collections.Generic;
using DemoApplication.Models;
using DemoApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UsersController : ControllerBase
{ 
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
          _userService = userService;
    }
      
    // POST: api/user/login
    // Define the different response types. TODO define the response types for other API's
    [ProducesResponseType(typeof(LoginUserResponse), 200)]
    [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
    [ProducesResponseType(500)]
    [HttpPost]
    public IActionResult Login([FromBody] LoginUserRequest loginUserRequest)
    {
        return Ok(_userService.LoginUser(loginUserRequest));
    }
        
    // POST: api/user/register
    [HttpPost]
    public IActionResult Register([FromBody] RegisterUserRequest registerUserRequest)
    {
       return Ok(_userService.RegisterUser(registerUserRequest));
    }
        
}

