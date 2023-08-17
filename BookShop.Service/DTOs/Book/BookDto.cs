namespace BookShop.Service.DTOs.Book;

public class BookDto
{
    public required string UserName { get; set; }
    public required string GenreName {get; set; }
    public required string PublisherName {get; set; }
    public required string Name { get; set; }
    public int Isbn { get; set; }
    public decimal Price { get; set; }
    public ICollection<string>? BookImagePaths { get; set; }
}