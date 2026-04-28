using AutoMapper;
using TicketBooking.Application.DTOs.Order;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.MappingProfiles;
public class OrderAutoMapper : Profile
{
    public OrderAutoMapper()
    {
        CreateMap<Order, OrderGetDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<Order,OrderDetailDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
