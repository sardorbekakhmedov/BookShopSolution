using BookShop.Data.Repositories.GenericRepositories;
using BookShop.Service.DTOs.User;
using BookShop.Service.Managers;
using BookShop.Service.Managers.IManagers;
using BookShop.Service.Mappers;
using BookShop.Service.Services;
using BookShop.Service.Services.IServices;
using BookShop.Service.Services.Pagination;
using BookShop.Service.Validators;
using FluentValidation;
using UserManager = BookShop.Service.Managers.UserManager;

namespace BookShop.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddBookShopRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IGenreManager, GenreManager>();
        services.AddScoped<IDiscountManager, DiscountManager>();
        services.AddScoped<IPublisherManager, PublisherManager>();
        services.AddScoped<IBookManager, BookManager>();
    }

    public static void AddBookshopServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<HttpContextHelper>();
        services.AddHttpContextAccessor();
        services.AddAutoMapper(typeof(MapperProfile));
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
    }
}