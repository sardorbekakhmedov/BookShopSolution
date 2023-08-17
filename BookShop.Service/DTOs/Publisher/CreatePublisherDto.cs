using Microsoft.AspNetCore.Http;

namespace BookShop.Service.DTOs.Publisher;

public class CreatePublisherDto
{
    public  IFormFile? LogoPublisher { get; set; }
    public required string Name { get; set; }
}