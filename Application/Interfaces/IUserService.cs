using Domain.Entities;
using Infrastructure.DTOs;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<MemberDto>> GetUsersAsync();
    public Task<MemberDto> GetUserByUsernameAsync(string username);
    public Task<AppUser> UpdateUserAsync(string username,MemberUpdateDTO  memberUpdateDto);
}