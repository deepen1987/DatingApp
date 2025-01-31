using System.Security.Claims;
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

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDto)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if(username == null) return BadRequest("No username found in token");
        
        var member = await userService.GetUserByUsernameAsync(username);
        if(member == null) return NotFound("Could not find user");

        var user = await userService.UpdateUserAsync(username, memberUpdateDto);
        
        if(user == null) return BadRequest("Could not update user");
        return NoContent();
    }
}