using DemoApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Context;

public interface IApplicationDbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }

    int SaveChanges();
}