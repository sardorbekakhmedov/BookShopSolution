using AutoMapper;
using BookShop.Domain.Entities;
using BookShop.Service.DTOs.Discount;
using BookShop.Service.DTOs.Genre;

namespace BookShop.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Genre, CreateGenreDto>().ReverseMap();
        CreateMap<GenreDto, Genre>().ReverseMap();
        CreateMap<Discount, CreateDiscountDto>().ReverseMap();
        CreateMap<DiscountDto, Discount>().ReverseMap();
    }
}