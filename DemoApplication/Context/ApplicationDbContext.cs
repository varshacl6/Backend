using System;
using DemoApplication.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
  private readonly IHttpContextAccessor _httpContextAccessor;
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public DbSet<User> Users { get; set; }

  public DbSet<Movie> Movies { get; set; }

  public override int SaveChanges()
  {
    AddTimestamps();
    return base.SaveChanges();
  }

  private void AddTimestamps()
  {
    foreach (var entity in ChangeTracker.Entries())
    {
      bool hasChanged = entity.State == EntityState.Added || entity.State == EntityState.Modified;
      if (hasChanged)
      {
        if ((entity.Entity is BaseEntity) || (entity.Entity is BaseDateEntity))
        {
          DateTime now = DateTime.UtcNow;
          if (entity.State == EntityState.Added)
          {
            ((BaseDateEntity) entity.Entity).EnteredOn = now;
          }
          ((BaseDateEntity) entity.Entity).UpdatedOn = now;
        }

        if (entity.Entity is BaseEntity)
        {
          string username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Default";
          if (entity.State == EntityState.Added)
          {
            ((BaseEntity) entity.Entity).EnteredBy = username;
          }
          ((BaseEntity) entity.Entity).UpdatedBy = username;
        }

      }
    }
  }
}