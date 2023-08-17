using BookShop.Domain.Shared;
using BookShop.Service.DTOs.Book;
using BookShop.Service.Exceptions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.PageFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookManager _bookManager;

    public BooksController(IBookManager bookManager)
    {
        _bookManager = bookManager;
    }

    [HttpPost]
    [Authorize]
    public async ValueTask<IActionResult> InsertBook(CreateBookDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(dto);

        try
        {
            return Created("InsertBook",  await _bookManager.InsertBookAsync(dto));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }


    [HttpPatch("{bookId:guid}/image")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> AddUserImage(Guid bookId, [FromForm] IFormFile[] images)
    {
        try
        {
            await _bookManager.SaveBookImagesAsync(bookId, images);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAllBooks([FromQuery] BookFilter filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(filter);

        try
        {
            return Ok(await _bookManager.GetAllBooksAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{bookId:guid}")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> GetBookById(Guid bookId)
    {
        try
        {
            return Ok(await _bookManager.GetBookByIdAsync(bookId));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut("{bookId:guid}")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> UpdateBook(Guid bookId, UpdateBookDto dto)
    {
        try
        {
            return Ok(await _bookManager.UpdateBookAsync(bookId, dto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete("{bookId:guid}")]
    [Authorize(Policy = UserRoles.SuperAdmin)]
    public async ValueTask<IActionResult> DeleteBook(Guid bookId)
    {
        try
        {
            await _bookManager.DeleteBookAsync(bookId);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}