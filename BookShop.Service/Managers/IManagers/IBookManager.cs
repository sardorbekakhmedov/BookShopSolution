using BookShop.Service.DTOs.Book;
using BookShop.Service.Services.PageFilters;
using Microsoft.AspNetCore.Http;

namespace BookShop.Service.Managers.IManagers;

public interface IBookManager
{
    ValueTask<BookDto> InsertBookAsync(CreateBookDto dto);
    ValueTask SaveBookImagesAsync(Guid bookId, IFormFile[] images);
    ValueTask<IEnumerable<BookDto>> GetAllBooksAsync(BookFilter filter);
    ValueTask<BookDto> GetBookByIdAsync(Guid bookId);
    ValueTask<BookDto> UpdateBookAsync(Guid bookId, UpdateBookDto dto);
    ValueTask DeleteBookAsync(Guid bookId);
}