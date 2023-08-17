using BookShop.Domain.Shared;

namespace BookShop.Domain.Entities;

public class User : BaseEntity
{
    public string? Firstname { get; set; } 
    public required string Username { get; set; }
    public string PasswordHash { get; set; } = null!;
     
    public ICollection<Image>? UserImages { get; set; }
    public virtual ICollection<Role>? Roles { get; set; }
    public virtual ICollection<Book>? Books { get; set; }
}