using AutoMapper;
using TicketBooking.Application.DTOs.Ticket;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.MappingProfiles;
public class TicketAutoMapper : Profile
{
    public TicketAutoMapper()
    {
        CreateMap<Ticket, TicketGetDto>()
            .ForMember(dest => dest.EventTitle, opt => opt.MapFrom(src => src.Event.Title))
            .ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.Event.Venue.Name));
        CreateMap<TicketCreateDto, Ticket>();
    }
}
