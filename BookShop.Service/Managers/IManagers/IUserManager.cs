using BookShop.Service.DTOs.User;
using BookShop.Service.Services.PageFilters;
using Microsoft.AspNetCore.Http;

namespace BookShop.Service.Managers.IManagers;

public interface IUserManager
{
    ValueTask<UserDto> InsertAsync(CreateUserDto dto);
    ValueTask SaveUserImagesAsync(Guid userId, IFormFile[] images);
    ValueTask DeleteImageAsync(Guid userImageId);
    ValueTask<IEnumerable<UserDto>> GetAllUsersAsync(UserFilter filter);
    ValueTask<(string, double)> LoginAsync(string username, string password);
    ValueTask<UserDto> GetByIdAsync(Guid userId);
    ValueTask<UserDto> GetByUsernameAsync(string username);
    ValueTask<UserDto> UpdateAsync(Guid userId, UpdateUserDto dto);
    ValueTask DeleteAsync(Guid userId);
}