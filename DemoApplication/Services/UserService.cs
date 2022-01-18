using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AuthenticationPlugin;
using DemoApplication.Data;
using DemoApplication.Entities;
using DemoApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace DemoApplication.Services;

public class UserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly AuthService _auth;
    
    public UserService(ApplicationDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _auth = new AuthService(configuration);
    }
    public User RegisterUser(RegisterUserRequest registerUserRequest)
    {
        var existingUserWithSameUserName = _dbContext.Users
            .SingleOrDefault(user => user.Username == registerUserRequest.Username);
        if (existingUserWithSameUserName!=null)
        {
            throw new BadHttpRequestException("User with same username already exists ");
        }

        User newUser = new User
        {
            Username = registerUserRequest.Username,
            FirstName = registerUserRequest.FirstName,
            LastName = registerUserRequest.LastName,
            Password = BCrypt.Net.BCrypt.HashPassword(registerUserRequest.Password),
            Role = "Users"
        };
        EntityEntry<User> savedUserEntity = _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
        return savedUserEntity.Entity;
    }

    public LoginUserResponse LoginUser(LoginUserRequest loginUserRequest)
    {
        var userByUserName = _dbContext.Users
            .SingleOrDefault(user => user.Username == loginUserRequest.Username);
        if (userByUserName == null|| !BCrypt.Net.BCrypt.Verify(loginUserRequest.Password, userByUserName.Password))
        {
            throw new UnauthorizedAccessException("Forbidden");
        }
        
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.UniqueName, loginUserRequest.Username)
        };
        var token = _auth.GenerateAccessToken(claims);
       return new LoginUserResponse()
        {
            accessToken = token.AccessToken,
            expiresIn = token.ExpiresIn,
            tokenType = token.TokenType,
            creationTime = token.ValidFrom,
            expirationTime = token.ValidTo,
            username = loginUserRequest.Username
        };
    }
}