using Microsoft.AspNetCore.Http;

namespace BookShop.Service.Services.IServices;

public interface IFileService
{
    public Task<string> SaveFileAsync(IFormFile formFile, string folderName);
    public void Delete(string filePath);
}