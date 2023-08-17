namespace BookShop.Service.DTOs.User;

public class UserDto
{
    public string? Firstname { get; set; }
    public required string Username { get; set; }

    public virtual ICollection<string> UserRoles { get; set; } = null!;
    public ICollection<string> UserImagePaths { get; set; } = null!;
    public virtual ICollection<string> Books { get; set; } = null!;
}