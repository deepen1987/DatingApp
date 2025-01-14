using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.DataContext;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}