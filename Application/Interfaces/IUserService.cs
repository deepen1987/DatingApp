using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<AppUser>> GetUsers();
    public Task<AppUser> GetUserById(int id);
}