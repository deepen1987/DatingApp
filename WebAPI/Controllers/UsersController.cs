using Application.Interfaces;
using AutoMapper;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
public class UsersController(IUserService userService) : CustomControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userService.GetUsersAsync();
        
        return Ok(users);
    }
    
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await userService.GetUserByUsernameAsync(username);
        if(user == null) return NotFound();
        
        return Ok(user);
    }
}