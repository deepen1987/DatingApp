using Domain.Entities;
using Infrastructure.DTOs;

namespace WebAPI.Interfaces;

public interface IUserService
{
    public Task<IEnumerable<MemberDto>> GetUsersAsync();
    public Task<MemberDto?> GetMemberByUsernameAsync(string username);
    public Task<AppUser?> GetUserByUsernameAsync(string username);
    public Task<AppUser?> UpdateUserAsync(string username,MemberUpdateDTO  memberUpdateDto);
    public Task<bool> SaveAllAsyncMember(MemberDto memberDto);
    public Task<bool> SaveAllAsync();
}