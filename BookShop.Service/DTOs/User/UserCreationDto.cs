using Microsoft.AspNetCore.Http;

namespace BookShop.Service.DTOs.User;

public class UserCreationDto
{
    public IFormFile? UserImage { get; set; }
    public string? Firstname { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}