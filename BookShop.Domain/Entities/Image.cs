using BookShop.Domain.Shared;

namespace BookShop.Domain.Entities;

public class Image : BaseEntity
{
    public required string ImagePath { get; set; }
}