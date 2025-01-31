using Application.Interfaces;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class UserService(IUserRepository userRepository, DapperDbContext dapperContext) : IUserService
{
    public async Task<IEnumerable<MemberDto>> GetUsersAsync()
    {
        var users = await userRepository.GetMembersAsync();
        return users;
    }

    public async Task<MemberDto> GetUserByUsernameAsync(string username)
    {
        var user = await userRepository.GetMemberAsync(username);
        
        return user;
    }

    public async Task<AppUser> UpdateUserAsync(string username, MemberUpdateDTO  memberUpdateDto)
    {
        var user = await userRepository.UpdateUserAsync(username, memberUpdateDto);
        return user;
    }
}