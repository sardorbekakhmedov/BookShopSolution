using BookShop.Domain.Shared;

namespace BookShop.Domain.Entities;

public class Genre : BaseEntity
{
    public required string GenreName { get; set; }
}