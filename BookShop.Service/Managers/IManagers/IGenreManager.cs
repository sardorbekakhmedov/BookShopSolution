using BookShop.Service.DTOs.Genre;
using BookShop.Service.Services.PageFilters;

namespace BookShop.Service.Managers.IManagers;

public interface IGenreManager
{
    ValueTask<GenreDto> InsertGenreAsync(CreateGenreDto dto);
    ValueTask<IEnumerable<GenreDto>> GetAllGenresAsync(GenreFilter filter);
    ValueTask<GenreDto> GetByIdAsync(Guid genreId);
    ValueTask<GenreDto> UpdateGenreAsync(Guid genreId, UpdateGenreDto dto);
    ValueTask DeleteGenreAsync(Guid genreId);
}