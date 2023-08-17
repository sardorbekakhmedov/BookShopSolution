namespace BookShop.Service.DTOs.Publisher;

public class PublisherDto
{
    public string? LogoPath { get; set; }
    public required string Name { get; set; }

    public ICollection<string> BookNames { get; set; }

    public PublisherDto()
    {
        BookNames = new HashSet<string>();
    }
}