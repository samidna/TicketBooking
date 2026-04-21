using AutoMapper;
using TicketBooking.Application.DTOs.Venue;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.MappingProfiles;
public class VenueAutoMapper : Profile
{
    public VenueAutoMapper()
    {
        CreateMap<Venue, VenueGetDto>()
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));
        CreateMap<VenueCreateDto, Venue>();
    }
}
