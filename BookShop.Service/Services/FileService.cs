using BookShop.Service.Services.IServices;
using Microsoft.AspNetCore.Http;

namespace BookShop.Service.Services;

public class FileService : IFileService
{
    private readonly string _rootFolderName = Path.Combine(Environment.CurrentDirectory, "wwwroot");

    private string CheckFolder(string folderName)
    {
        if(!Directory.Exists(_rootFolderName))
            Directory.CreateDirectory(_rootFolderName);

        var folderPath = Path.Combine(_rootFolderName, folderName);

        if(!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        return folderPath;
    }

    public async Task<string> SaveFileAsync(IFormFile formFile, string folderName)
    {
        var folderPath = CheckFolder(folderName);

        var fileName = Guid.NewGuid() + Path.GetExtension(formFile.FileName);

        var filePath = Path.Combine(folderPath, fileName);

        await using var filStream = new FileStream(filePath, FileMode.Create);
        await formFile.CopyToAsync(filStream);

        return filePath;
    }

    public void Delete(string filePath)
    {
        var file = Path.Combine(Environment.CurrentDirectory, filePath);

        if(File.Exists(file))
            File.Delete(file);
    }
}