using BookShop.Data.Repositories.GenericRepositories;
using BookShop.Domain.Entities;
using BookShop.Service.DTOs.Book;
using BookShop.Service.Exceptions;
using BookShop.Service.Extensions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.IServices;
using BookShop.Service.Services.PageFilters;
using BookShop.Service.Services.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookShop.Service.Managers;

public class BookManager : IBookManager
{
    private const string BookImages = "BookImages";
    private readonly IFileService _fileService;
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IGenericRepository<Image> _imageRepository;
    private readonly IGenericRepository<Book> _bookRepository;

    public BookManager(IFileService fileService,HttpContextHelper httpContextHelper,
        IGenericRepository<Image> imageRepository,
        IGenericRepository<Book> bookRepository)
    {
        _fileService = fileService;
        _httpContextHelper = httpContextHelper;
        _imageRepository = imageRepository;
        _bookRepository = bookRepository;
    }

    public async ValueTask<BookDto> InsertBookAsync(CreateBookDto dto)
    {
        var book = new Book
        {
            Name = dto.Name,
            GenreId = dto.GenreId,
            PublisherId = dto.PublisherId,
            UserId = dto.UserId,
            Isbn = dto.Isbn,
            Price = dto.Price,
            BookImages = new HashSet<Image>()
        };

        if (dto.BookImage is not null)
        {
            book.BookImages.Add(await SaveImageAsync(dto.BookImage));
        }

        var newBook = await _bookRepository.InsertAsync(book);

        var bookWithNavigation = await GetBookQueryWithNavigationProperties()
            .SingleOrDefaultAsync(b => b.Id.Equals(newBook.Id));

        return bookWithNavigation is null ? throw new NotFoundException($"{nameof(Book)} not found!")
            :  bookWithNavigation.ToBookDto();
    }

    public async ValueTask SaveBookImagesAsync(Guid bookId, IFormFile[] images)
    {
        var book = await GetBookQueryWithNavigationProperties()
            .SingleOrDefaultAsync(b => b.Id.Equals(bookId));

        if (book is null)
            throw new NotFoundException("Book not found!");

        book.BookImages ??= new HashSet<Image>();

        foreach (var image in images)
        {
            var newImage = await SaveImageAsync(image);

            book.BookImages.Add(newImage);
        }
        await _bookRepository.UpdateAsync(book);
    }


    public async ValueTask<IEnumerable<BookDto>> GetAllBooksAsync(BookFilter filter)
    {
        var query = GetBookQueryWithNavigationProperties();

        if (filter.BookName is not null)
            query = query.Where(b => b.Name.ToLower().Contains(filter.BookName.ToLower()));

        if (filter.AuthorName is not null)
            query = query.Where(b => b.User.Username.ToLower().Contains(filter.AuthorName.ToLower()));

        if (filter.PublisherName is not null)
            query = query.Where(b => b.Publisher.Name.ToLower().Contains(filter.PublisherName.ToLower()));

        if (filter.FromDate is not null)
            query = query.Where(b => b.CreatedDate >= filter.FromDate);

        if (filter.ToDate is not null)
            query = query.Where(b => b.CreatedDate >= filter.ToDate);

        var books = await query.ToPagedListAsync(_httpContextHelper, filter);

        return books.Select(book => book.ToBookDto());
    }

    public async ValueTask<BookDto> GetBookByIdAsync(Guid bookId)
    {
        var book = await GetBookQueryWithNavigationProperties()
            .SingleOrDefaultAsync(b => b.Id.Equals(bookId));

        return book is null ? throw new NotFoundException($"{nameof(Book)} not found!")
            : book.ToBookDto();
    }
    
    public async ValueTask<BookDto> UpdateBookAsync(Guid bookId, UpdateBookDto dto)
    {
        var book = await GetBookQueryWithNavigationProperties()
            .SingleOrDefaultAsync(b => b.Id.Equals(bookId));

        if (book is null)
            throw new NotFoundException($"{nameof(Book)} not found!");

        book.GenreId = dto.GenreId ?? book.GenreId;
        book.PublisherId = dto.PublisherId ?? book.PublisherId;
        book.UserId = dto.UserId ?? book.UserId;
        book.Name = dto.Name ?? book.Name;
        book.Isbn = dto.Isbn ?? book.Isbn;
        book.Price = dto.Price ?? book.Price;

        var updatedBook = await _bookRepository.UpdateAsync(book);

        return updatedBook.ToBookDto();
    }

    public async ValueTask DeleteBookAsync(Guid bookId)
    {
        var book = await GetBookQueryWithNavigationProperties()
            .SingleOrDefaultAsync(b => b.Id.Equals(bookId));

        if (book is not null)
            await _bookRepository.DeleteAsync(book);
        else
            throw new NotFoundException($"{nameof(Book)} not found!");
    }


    private IQueryable<Book> GetBookQueryWithNavigationProperties()
    {
        return _bookRepository.SelectAll()
            .Include(u => u.User)
            .Include(p => p.Publisher)
            .Include(g => g.Genre);
    }
   
    private async ValueTask<Image> SaveImageAsync(IFormFile imageFile)
    {
        var image = new Image()
        {
            ImagePath = await _fileService.SaveFileAsync(imageFile, BookImages),
        };

        return await _imageRepository.InsertAsync(image);
    }
}