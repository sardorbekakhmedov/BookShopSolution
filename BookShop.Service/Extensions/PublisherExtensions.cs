using BookShop.Domain.Entities;
using BookShop.Service.DTOs.Publisher;

namespace BookShop.Service.Extensions;

public static class PublisherExtensions
{
    public static PublisherDto ToPublisherDto(this Publisher publisher)
    {
        var publisherDto = new PublisherDto
        {
            Name = publisher.Name,
            LogoPath = publisher.LogoPath,
        };

        if (publisher.Books is null)
            return publisherDto;

        foreach (var book in publisher.Books)
        {
            publisherDto.BookNames.Add(book.Name);
        }

        return publisherDto;
    }
}