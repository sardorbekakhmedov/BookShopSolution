using Microsoft.AspNetCore.Http;

namespace BookShop.Service.DTOs.Publisher;

public class UpdatePublisherDto
{
    public IFormFile? LogoPath { get; set; }
    public string? Name { get; set; }
}