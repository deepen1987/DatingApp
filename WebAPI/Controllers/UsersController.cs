using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class UsersController(IUserService userService) : CustomControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        return Ok(userService.GetUsers());
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<AppUser> GetUser(int id)
    {
        var user = userService.GetUserById(id);
        if(user == null) return NotFound();
        return Ok(user);
    }
}