using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Interfaces;

namespace WebAPI.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = config["TokenKey"] ?? throw new Exception("TokenKey not found in appsettings");
        if (tokenKey.Length < 64) throw new Exception("TokenKey must be long");
        
        //Here we are creating key using SymmetricSecurityKey from IdentityModel.Tokens
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((tokenKey)));

        // In short claim is information user is will be claiming he is 
        var claim = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName),
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claim),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken((tokenDescriptor));
        
        return tokenHandler.WriteToken(token);
    }
}