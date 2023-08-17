using BookShop.Domain.Shared;

namespace BookShop.Domain.Entities;

public class Publisher : BaseEntity
{
    public string? LogoPath { get; set; }
    public required string Name { get; set; }

    public ICollection<Book>? Books { get; set; }
}