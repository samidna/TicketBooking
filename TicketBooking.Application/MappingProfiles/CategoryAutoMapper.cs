using AutoMapper;
using TicketBooking.Application.DTOs.Category;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.MappingProfiles;
public class CategoryAutoMapper : Profile
{
    public CategoryAutoMapper()
    {
        CreateMap<Category, CategoryGetDto>().ReverseMap();
        CreateMap<CategoryCreateDto, Category>();
    }
}
