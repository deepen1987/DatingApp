using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class UsersController(IUserService userService) : CustomControllerBase
{
    
    [HttpGet("users")]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        return Ok(userService.GetUsers());
    }
    
    [HttpGet("{id}")]
    public ActionResult<AppUser> GetUser(int id)
    {
        var user = userService.GetUserById(id);
        if(user == null) return NotFound();
        return Ok(user);
    }
}