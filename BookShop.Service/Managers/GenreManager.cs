using AutoMapper;
using BookShop.Data.Repositories.GenericRepositories;
using BookShop.Domain.Entities;
using BookShop.Service.DTOs.Genre;
using BookShop.Service.Exceptions;
using BookShop.Service.Extensions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.PageFilters;
using BookShop.Service.Services.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Service.Managers;

public class GenreManager : IGenreManager
{
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Genre> _genericRepository;

    public GenreManager(HttpContextHelper httpContextHelper,IMapper mapper,IGenericRepository<Genre> genericRepository)
    {
        _httpContextHelper = httpContextHelper;
        _mapper = mapper;
        _genericRepository = genericRepository;
    }

    public async ValueTask<GenreDto> InsertGenreAsync(CreateGenreDto dto)
    {
        var genre = _mapper.Map<Genre>(dto);

        var newGenre = await _genericRepository.InsertAsync(genre);

        return _mapper.Map<GenreDto>(newGenre);
    }

    public async ValueTask<IEnumerable<GenreDto>> GetAllGenresAsync(GenreFilter filter)
    {
        var query = _genericRepository.SelectAll();

        if (filter.GenreName is not null)
            query = query.Where(g => g.GenreName.ToLower().Contains(filter.GenreName.ToLower()));

        if (filter.FromDate is not null)
            query = query.Where(g => g.CreatedDate >= filter.FromDate);

        if (filter.ToDate is not null)
            query = query.Where(g => g.CreatedDate <= filter.ToDate);

        var genres = await query.AsNoTracking().ToPagedListAsync(_httpContextHelper, filter);

        return genres.Select(genre => _mapper.Map<GenreDto>(genre)).ToList();
    }

    public async ValueTask<GenreDto> GetByIdAsync(Guid genreId)
    {
        var genre = await GetGenreByIdAsync(genreId);
        return _mapper.Map<GenreDto>(genre);
    }

    public async ValueTask<GenreDto> UpdateGenreAsync(Guid genreId,UpdateGenreDto dto)
    {
        var genre = await GetGenreByIdAsync(genreId);

        var updateGenre = await _genericRepository.UpdateAsync(genre);

        return _mapper.Map<GenreDto>(updateGenre);
    }

    public async ValueTask DeleteGenreAsync(Guid genreId)
    {
        var genre = await GetGenreByIdAsync(genreId);

        await _genericRepository.DeleteAsync(genre);
    }

    private async ValueTask<Genre> GetGenreByIdAsync(Guid genreId)
    {
        var genre = await _genericRepository.SelectSingleAsync(g => g.Id.Equals(genreId)) 
                    ?? throw new NotFoundException($"{nameof(Genre)} not found");

        return genre;
    }
}