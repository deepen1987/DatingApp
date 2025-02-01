using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.DataContext;
using Infrastructure.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(DatabaseContext context, IMapper mapper) : IUserRepository
{
    public void Update(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<AppUser>> GetUserAsync()
    {
        return await context.Users.
            Include(x => x.Photos).ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await context.Users
            .Include(x => x.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await context.Users
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<MemberDto?> GetMemberAsync(string username)
    {
        return await context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<AppUser?> UpdateUserAsync(string username,MemberUpdateDTO  memberUpdateDto)
    {
        var user = await GetUserByUsernameAsync(username);
        if (user == null) return null;
        
        mapper.Map(memberUpdateDto, user);
        
        if(await SaveAllAsync()) return user;
        
        return null;
    }
    
    public async Task<bool> SaveAllAsyncMember(MemberDto memberDto)
    {
        var user = await GetUserByUsernameAsync(memberDto.Username);
        if (user == null) return false;
        
        mapper.Map(memberDto, user);
        
        return await SaveAllAsync();
    }
}