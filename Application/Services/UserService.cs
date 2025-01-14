using Application.Interfaces;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService(DatabaseContext context) : IUserService
{
    public async Task<IEnumerable<AppUser>> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return users;
    }

    public async Task<AppUser> GetUserById(int id)
    {
        var user = await context.Users.FindAsync(id);
        
        return user;
    }
}