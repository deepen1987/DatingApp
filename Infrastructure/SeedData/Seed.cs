using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Domain.Entities;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData;

public class Seed
{
    public static async Task SeedUsers(DatabaseContext context)
    {
        if (await context.Users.AnyAsync()) return;
        
        var userData = await File.ReadAllTextAsync("/Users/deependrashekhawat/RiderProjects/DatingApp/Infrastructure/SeedData/UserSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

        if(users == null) return;
        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;
            
            context.Users.Add(user);
            
        }
        
        await context.SaveChangesAsync();
    }
}