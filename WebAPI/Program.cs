using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. This is the dependency injection Services Stack

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// **** Middleware Stack ****
// This is the middleware stack which helps to configure the HTTP request pipeline In / Out of the application.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200/"));

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();