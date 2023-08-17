using BookShop.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BookShop.Service.DTOs.Book;

public class CreateBookDto
{
    public IFormFile? BookImage { get; set; }
    public required Guid UserId { get; set; }
    public required Guid GenreId { get; set; }
    public required Guid PublisherId { get; set; }

    public required string Name { get; set; }
    public int Isbn { get; set; }
    public decimal Price { get; set; }
}