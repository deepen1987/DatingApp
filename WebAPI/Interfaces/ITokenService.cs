using Domain.Entities;

namespace WebAPI.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}