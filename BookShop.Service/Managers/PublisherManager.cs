using BookShop.Data.Repositories.GenericRepositories;
using BookShop.Domain.Entities;
using BookShop.Service.DTOs.Publisher;
using BookShop.Service.Exceptions;
using BookShop.Service.Extensions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.IServices;
using BookShop.Service.Services.PageFilters;
using BookShop.Service.Services.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Service.Managers;

public class PublisherManager : IPublisherManager
{
    private const string PublisherLogos = "PublisherLogos";
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IFileService _fileService;
    private readonly IGenericRepository<Publisher> _publisherRepository;

    public PublisherManager(HttpContextHelper httpContextHelper,IFileService fileService,IGenericRepository<Publisher> publisherRepository)
    {
        _httpContextHelper = httpContextHelper;
        _fileService = fileService;
        _publisherRepository = publisherRepository;
    }

    public async ValueTask<PublisherDto> InsertPublisherAsync(CreatePublisherDto dto)
    {
        var publisher = new Publisher { Name = dto.Name };

        if (dto.LogoPublisher is not null)
            publisher.LogoPath = await _fileService.SaveFileAsync(dto.LogoPublisher, PublisherLogos);

        var newPublisher = await _publisherRepository.InsertAsync(publisher);

        return newPublisher.ToPublisherDto();
    }

    public async ValueTask<IEnumerable<PublisherDto>> GetAllPublishersAsync(PublisherFilter filter)
    {
        var query = GetPublisherWithNavigationProperties();

        if (filter.PublisherName is not null)
            query = query.Where(p => p.Name.ToLower().Contains(filter.PublisherName.ToLower()));

        if (filter.FromDate is not null)
            query = query.Where(p => p.CreatedDate >= filter.FromDate);

        if (filter.ToDate is not null)
            query = query.Where(p => p.CreatedDate <= filter.ToDate);

        var publishers = await query.ToPagedListAsync(_httpContextHelper, filter);

        return publishers.Select(p => p.ToPublisherDto());
    }

    public async ValueTask<PublisherDto> GetPublisherByIdAsync(Guid publisherId)
    {
        var publisher = await GetPublisherWithNavigationProperties()
            .SingleOrDefaultAsync(p => p.Id.Equals(publisherId));

        return publisher is null
            ? throw new NotFoundException($"{nameof(Publisher)} not found!")
            : publisher.ToPublisherDto();
    }

    public async ValueTask<PublisherDto> UpdatePublisherAsync(Guid publisherId, UpdatePublisherDto dto)
    {
        var publisher = await GetPublisherWithNavigationProperties()
            .SingleOrDefaultAsync(p => p.Id.Equals(publisherId));

        if (publisher is null)
            throw new NotFoundException($"{nameof(Publisher)} not found!");

        publisher.Name = dto.Name ?? publisher.Name;

        if (dto.LogoPath is not null)
        {
            if (publisher.LogoPath is not null)
                _fileService.Delete(publisher.LogoPath);

            publisher.LogoPath = await _fileService.SaveFileAsync(dto.LogoPath, PublisherLogos);
        }

        await _publisherRepository.UpdateAsync(publisher);

        return publisher.ToPublisherDto();
    }

    public async ValueTask DeletePublisherAsync(Guid publisherId)
    {
        var publisher = await GetPublisherWithNavigationProperties()
            .SingleOrDefaultAsync(p => p.Id.Equals(publisherId));

        if (publisher is null)
            throw new NotFoundException($"{nameof(Publisher)} not found!");

        if (publisher.LogoPath is not null)
            _fileService.Delete(publisher.LogoPath);

        await _publisherRepository.DeleteAsync(publisher);
    }

    private IQueryable<Publisher> GetPublisherWithNavigationProperties()
    {
        return _publisherRepository.SelectAll().Include(p => p.Books).AsQueryable();
    }
}