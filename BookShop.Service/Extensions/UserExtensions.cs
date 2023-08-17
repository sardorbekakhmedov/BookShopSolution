using BookShop.Domain.Entities;
using BookShop.Service.DTOs.User;

namespace BookShop.Service.Extensions;

public static class UserExtensions
{
    public static UserDto ToUserDto(this User user)
    {
        var userDto = new UserDto()
        {
            Firstname = user.Firstname,
            Username = user.Username,
            UserRoles = new HashSet<string>(),
            UserImagePaths = new HashSet<string>(),
            Books = new HashSet<string>()
        };

        if (user.Roles is not null && user.Roles.Count > 0)
        {
            foreach (var role in user.Roles)
            {
                userDto.UserRoles.Add(role.Name);
            }
        }

        if (user.UserImages is not null && user.UserImages.Count > 0 )
        {
            foreach (var image in user.UserImages)
            {
                userDto.UserImagePaths.Add(image.ImagePath);
            }
        }

        if (user.Books is not null && user.Books.Count > 0)
        {
            foreach (var book in user.Books)
            {
                userDto.Books.Add(book.Name);
            }
        }

        return userDto;
    }
}