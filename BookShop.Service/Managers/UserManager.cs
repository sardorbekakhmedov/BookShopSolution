using BookShop.Data.Repositories.GenericRepositories;
using BookShop.Domain.Entities;
using BookShop.Domain.Shared;
using BookShop.Service.DTOs.User;
using BookShop.Service.Exceptions;
using BookShop.Service.Extensions;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Services.IServices;
using BookShop.Service.Services.PageFilters;
using BookShop.Service.Services.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Service.Managers;

public class UserManager : IUserManager
{
    private const string UserImages = "UserImages";
    private readonly IFileService _fileService;
    private readonly IJwtTokenService _jwtToken;
    private readonly HttpContextHelper _httpHelper;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly IGenericRepository<Image> _imageRepository;

    public UserManager(IFileService fileService, IJwtTokenService jwtToken, HttpContextHelper httpHelper,
        IGenericRepository<User> userRepository, 
        IGenericRepository<Role> roleRepository,
        IGenericRepository<Image> imageRepository)
    {
        _fileService = fileService;
        _jwtToken = jwtToken;
        _httpHelper = httpHelper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _imageRepository = imageRepository;
    }

    public async ValueTask<UserDto> InsertAsync(UserCreationDto dto)
    {
        var username = await _userRepository.CheckConditionAsync(u => u.Username.Equals(dto.Username));

        if (username)
            throw new ThisObjectAlreadyExistsException("This username already exists!");

        var user = new User()
        {
            Firstname = dto.Firstname,
            Username = dto.Username,
            UserImages = new HashSet<Image>(),
            Roles = new HashSet<Role>(),
            Books = new HashSet<Book>()
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, dto.Password);

        var userRole = await SaveRoleAsync();

        user.Roles.Add(userRole);

        if (dto.UserImage is not null)
        {
            var newImage =  await SaveImageAsync(dto.UserImage);
            user.UserImages.Add(newImage);
        }

        var newUser = await _userRepository.InsertAsync(user);

        return newUser.ToUserDto();
    }

    public async ValueTask SaveUserImagesAsync(Guid userId, IFormFile[] images)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Id.Equals(userId));

        if (user is null)
            throw new NotFoundException("User not found!");

        foreach (var image in images)
        {
            user.UserImages ??= new HashSet<Image>();
            var newImage = await SaveImageAsync(image);
            user.UserImages.Add(newImage);
        }
        await _userRepository.UpdateAsync(user);
    }

    public async ValueTask DeleteImageAsync(Guid userImageId)
    {
        var image = await _imageRepository.SelectSingleAsync(i => i.Id.Equals(userImageId));

        if (image is null)
            throw new NotFoundException("Image nor found!");

        await _imageRepository.DeleteAsync(image);
        _fileService.Delete(image.ImagePath);
    }

    public async ValueTask<(string, double)> LoginAsync(string username, string password)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Username.Equals(username));

        if (user is null)
            throw new NotFoundException("User not found!");

        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password);

        if (result is not PasswordVerificationResult.Success)
            throw new PasswordInCorrectException("Invalid password !");

        return _jwtToken.GenerateNewToken(user);
    }

    public async ValueTask<IEnumerable<UserDto>> GetAllUsersAsync(UserFilter filter)
    {
        var query = _userRepository.SelectAll()
            .Include(i => i.UserImages)
            .Include(r => r.Roles)
            .Include(b => b.Books).AsQueryable();

        if (filter.Username is not null)
            query = query.Where(u => u.Username.ToLower().Contains(filter.Username.ToLower()));

        if (filter.FromDate is not null)
            query = query.Where(u => u.CreatedDate >= filter.FromDate);

        if (filter.ToDate is not null)
            query = query.Where(u => u.CreatedDate <= filter.ToDate);

        var users = await query.AsNoTracking().ToPagedListAsync(_httpHelper, filter);

        return users.Select(u => u.ToUserDto());
    }

    public async ValueTask<UserDto> GetByIdAsync(Guid userId)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Id.Equals(userId));

        return user == null ? throw new NotFoundException("User not found!") : user.ToUserDto();
    }

    public async ValueTask<UserDto> GetByUsernameAsync(string username)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Username.Equals(username));

        return user == null ? throw new NotFoundException("User not found!") : user.ToUserDto();
    }

    public async ValueTask<UserDto> UpdateAsync(Guid userId, UserUpdateDto dto)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Id.Equals(userId)); ;

        if (user is null)
            throw new NotFoundException("User not found!");

        user.Username = dto.Username ?? user.Username;
        user.Firstname = dto.Firstname ?? user.Firstname;

        if (dto.Password is not null)
        {
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, dto.Password);
        }

        var updateUser = await _userRepository.UpdateAsync(user);

        return updateUser.ToUserDto();
    }

    public async ValueTask DeleteAsync(Guid userId)
    {
        var user = await _userRepository.SelectSingleAsync(u => u.Id.Equals(userId)); ;

        if (user is null)
            throw new NotFoundException("User not found!");

        if (user.UserImages is not null)
        {
            foreach (var image in user.UserImages)
            {
                _fileService.Delete(image.ImagePath);
            }
        }
        
        await _userRepository.DeleteAsync(user);
    }

    private async ValueTask<Role> SaveRoleAsync()
    {
        var userRole = await _roleRepository.SelectSingleAsync(r => r.Name.Equals(UserRoles.User));

        if (userRole is not null)
            return userRole;

        userRole = new Role() { Name = UserRoles.User };
        return await _roleRepository.InsertAsync(userRole);
    }

    private async ValueTask<Image> SaveImageAsync(IFormFile imageFile)
    {
        var image = new Image()
        {
            ImagePath = await _fileService.SaveFileAsync(imageFile, UserImages),
        };

        return await _imageRepository.InsertAsync(image);
    }
}