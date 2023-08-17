using BookShop.Domain.Shared;
using BookShop.Service.DTOs.Genre;
using BookShop.Service.Exceptions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.PageFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenreManager _genreManager;

    public GenresController(IGenreManager genreManager)
    {
        _genreManager = genreManager;
    }

    [HttpPost]
    [Authorize]
    public async ValueTask<IActionResult> InsertGenre([FromBody] CreateGenreDto dto)
    {
        try
        {
            return Created("InsertGenre", await _genreManager.InsertGenreAsync(dto));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> GetAllGenres([FromQuery] GenreFilter filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(filter);

        try
        {
            return Ok(await _genreManager.GetAllGenresAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{genreId}")]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> GetGenreById(Guid genreId)
    {
        try
        {
            return Ok(await _genreManager.GetByIdAsync(genreId));
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

    [HttpPut]
    [Authorize(Policy = UserRoles.User)]
    public async ValueTask<IActionResult> UpdateGenre(Guid genreId, UpdateGenreDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(dto);

        try
        {
            return Ok(await _genreManager.UpdateGenreAsync(genreId, dto));
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

    [HttpDelete]
    [Authorize(Policy = UserRoles.SuperAdmin)]
    public async ValueTask<IActionResult> DeleteGenre(Guid genreId, UpdateGenreDto dto)
    {
        try
        {
            await _genreManager.DeleteGenreAsync(genreId);
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