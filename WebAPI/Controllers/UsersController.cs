using AutoMapper;
using Domain.Entities;
using Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Extensions;
using WebAPI.Interfaces;

namespace WebAPI.Controllers;

[Authorize]
public class UsersController(IUserService userService, IPhotoService photoService, IMapper mapper) : CustomControllerBase
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
        var user = await userService.GetMemberByUsernameAsync(username);
        if(user == null) return NotFound();
        
        return Ok(user);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDto)
    {
        var username = User.GetUsername();

        var user = await userService.UpdateUserAsync(username, memberUpdateDto);
        
        if(user == null) return BadRequest("Could not update user");
        return NoContent();
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user = await userService.GetUserByUsernameAsync(User.GetUsername());
        
        if(user == null) return BadRequest("Could not add photo");
        
        var result = await photoService.AddPhotoAsync(file);
        
        if(result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };
        
        user.Photos.Add(photo);

        if (await userService.SaveAllAsync())
            return CreatedAtAction(nameof(GetUser),
        new {username = user.UserName
        },
        mapper.Map<PhotoDto>(photo));
        
        return BadRequest("Could not add photo");
    }

    [HttpPut("set-main-photo/{photoId:int}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user = await userService.GetUserByUsernameAsync(User.GetUsername());
        
        if(user == null) return BadRequest("Could not find user");
        
        var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
        
        if(photo == null || photo.IsMain) return BadRequest("Could not use this as main photo");
        
        var currentMain = user.Photos.FirstOrDefault(p => p.IsMain);
        if(currentMain != null) currentMain.IsMain = false;
        
        photo.IsMain = true;
        
        if(await userService.SaveAllAsync()) return NoContent();
        
        return BadRequest("Could not set main photo");
    }

    [HttpDelete("delete-photo/{photoId:int}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var user = await userService.GetUserByUsernameAsync(User.GetUsername());
        if(user == null) return BadRequest("Could not find user");
        
        var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);
        if(photo == null || photo.IsMain) return BadRequest("Could not delete photo");

        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if(result.Error != null) return BadRequest(result.Error.Message);
        }
        
        user.Photos.Remove(photo);
        if (await userService.SaveAllAsync()) return Ok();
        
        return BadRequest("Could not delete photo");
    }
}