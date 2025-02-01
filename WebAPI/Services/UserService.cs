using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using WebAPI.Interfaces;

namespace WebAPI.Services;

public class UserService(IUserRepository userRepository, DapperDbContext dapperContext) : IUserService
{
    public async Task<IEnumerable<MemberDto>> GetUsersAsync()
    {
        var users = await userRepository.GetMembersAsync();
        return users;
    }

    public async Task<MemberDto?> GetMemberByUsernameAsync(string username)
    {
        var user = await userRepository.GetMemberAsync(username);
        
        return user;
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await userRepository.GetUserByUsernameAsync(username);
    }

    public async Task<AppUser?> UpdateUserAsync(string username, MemberUpdateDTO  memberUpdateDto)
    {
        var user = await userRepository.UpdateUserAsync(username, memberUpdateDto);

        return user;
    }

    public async Task<bool> SaveAllAsyncMember(MemberDto memberDto)
    {
        return await userRepository.SaveAllAsyncMember(memberDto);
    }

    public Task<bool> SaveAllAsync()
    {
        return userRepository.SaveAllAsync();
    }
}