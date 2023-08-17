using BookShop.Service.DTOs.Publisher;
using BookShop.Service.Services.PageFilters;

namespace BookShop.Service.Managers.IManagers;

public interface IPublisherManager
{
    ValueTask<PublisherDto> InsertPublisherAsync(CreatePublisherDto dto);
    ValueTask<IEnumerable<PublisherDto>> GetAllPublishersAsync(PublisherFilter filter);
    ValueTask<PublisherDto> GetPublisherByIdAsync(Guid publisherId);
    ValueTask<PublisherDto> UpdatePublisherAsync(Guid publisherId, UpdatePublisherDto dto);
    ValueTask DeletePublisherAsync(Guid publisherId);
}