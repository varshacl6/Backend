using DemoApplication.Context;
using DemoApplication.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoApplication.Repository;

public class UserRepository
{
    private readonly ApplicationDbContext _dbContext;
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User? GetUserByUserName(string username)
    {
       return _dbContext.Users
            .SingleOrDefault(user => user.Username == username);
    }
    
    public User CreateUser(User newUser)
    {
        EntityEntry<User> savedUserEntity = _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
        return savedUserEntity.Entity;
    }
}