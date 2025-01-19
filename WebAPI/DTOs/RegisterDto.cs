using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs;

public class RegisterDto
{
    [Required (ErrorMessage = "Email is required")] 
    public string Username { get; set; } = string.Empty;
    [Required] 
    [StringLength(8, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 8 characters")]
    public string Password { get; set; } = string.Empty;
}