using Application.Interfaces;
using Application.Services;
using Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions;

public static class ApplicationServiceExtensions
{
    // This is an extension method which we create for IService Collection so that we can move our services code from Program.cs class.
    //
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<DapperDbContext>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }
}