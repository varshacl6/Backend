using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AuthenticationPlugin;
using DemoApplication.Context;
using DemoApplication.Entities;
using DemoApplication.Models;
using DemoApplication.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace DemoApplication.Services;

public class UserService
{
    private readonly AuthService _auth;
    private readonly UserRepository _userRepository;

    public UserService(IConfiguration configuration, UserRepository userRepository)
    {
        _auth = new AuthService(configuration);
        _userRepository = userRepository;
    }
    public User RegisterUser(RegisterUserRequest registerUserRequest)
    {
        User? existingUserWithSameUserName = _userRepository.GetUserByUserName(registerUserRequest.Username);
        if (existingUserWithSameUserName != null)
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
        return _userRepository.CreateUser(newUser);
    }

    public LoginUserResponse LoginUser(LoginUserRequest loginUserRequest)
    {
        User? userByUserName = _userRepository.GetUserByUserName(loginUserRequest.Username);
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