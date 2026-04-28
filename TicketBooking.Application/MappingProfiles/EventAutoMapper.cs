using AutoMapper;
using TicketBooking.Application.DTOs.Event;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.MappingProfiles;
public class EventAutoMapper : Profile
{
    public EventAutoMapper()
    {
        CreateMap<Event, EventDetailDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.Venue.Name))
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Venue.City.Name));
        CreateMap<Event, EventGetDto>();
        CreateMap<EventCreateDto, Event>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
        CreateMap<EventUpdateDto, Event>()
            .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
    }
}
