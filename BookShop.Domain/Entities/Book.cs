using BookShop.Domain.Shared;

namespace BookShop.Domain.Entities;

public class Book : BaseEntity
{
    public required Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public required Guid GenreId { get; set; }
    public virtual Genre Genre { get; set; } = null!;
    public required Guid PublisherId { get; set; }
    public virtual Publisher Publisher { get; set; } = null!;

    public required string Name { get; set; }
    public int Isbn { get; set; }
    public decimal Price { get; set; }
    public ICollection<Image>? BookImages { get; set; }
}