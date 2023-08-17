namespace BookShop.Service.DTOs.Book;

public class UpdateBookDto
{
    public Guid? UserId { get; set; }
    public Guid? GenreId { get; set; }
    public Guid? PublisherId { get; set; }

    public string? Name { get; set; }
    public int? Isbn { get; set; }
    public decimal? Price { get; set; }
}