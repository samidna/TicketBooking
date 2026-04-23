using AutoMapper;
using TicketBooking.Application.DTOs.City;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.MappingProfiles;
public class CityAutoMapper : Profile
{
    public CityAutoMapper()
    {
        CreateMap<City, CityGetDto>().ReverseMap();
        CreateMap<CityCreateDto, City>();
        CreateMap<CityUpdateDto, City>();
    }
}
