using BookShop.Domain.Entities;
using BookShop.Service.DTOs.Book;

namespace BookShop.Service.Extensions;

public static class BookExtensions
{
    public static BookDto ToBookDto(this Book book)
    {
        var bookDto = new BookDto
        {
            Name = book.Name,
            GenreName = book.Genre.GenreName,
            PublisherName = book.Publisher.Name,
            UserName = book.User.Username,
            Isbn = book.Isbn,
            Price = book.Price
        };

        if (book.BookImages is null) 
            return bookDto;

        bookDto.BookImagePaths = new HashSet<string>();

        foreach (var bookImagePath in book.BookImages)
            bookDto.BookImagePaths.Add(bookImagePath.ImagePath);

        return bookDto;
    }
}