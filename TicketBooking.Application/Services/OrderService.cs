using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Application.DTOs.Order;
using TicketBooking.Application.Exceptions;
using TicketBooking.Application.Interfaces;
using TicketBooking.Core.Entities;
using TicketBooking.Core.Enums;
using TicketBooking.Core.Interfaces;

namespace TicketBooking.Application.Services;
public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public OrderService(IMapper mapper, IUnitOfWork uow)
    {
        _mapper = mapper;
        _uow = uow;
    }

    public async Task<OrderGetDto> CreateOrderAsync(OrderCreateDto dto)
    {
        var eventEntity = await _uow.Events.GetSingleAsync(x => x.Id == dto.EventId, "Venue");

        if (eventEntity == null) throw new NotFoundException("Event not found.");

        int soldTicketsCount = await _uow.Tickets.GetWhere(x => x.EventId == dto.EventId).CountAsync();
        if (soldTicketsCount + dto.TicketCount > eventEntity.Venue.Capacity)
            throw new BadRequestException($"Only {eventEntity.Venue.Capacity - soldTicketsCount} tickets available for this event.");

        var order = new Order
        {
            AppUserId = dto.AppUserId,
            CreatedAt = DateTime.Now,
            Status = OrderStatus.Completed,
            TotalAmount = eventEntity.Price * dto.TicketCount,
            Tickets = new List<Ticket>()
        };

        for (int i = 0; i < dto.TicketCount; i++)
        {
            order.Tickets.Add(new Ticket
            {
                EventId = dto.EventId,
                TicketNumber = "T-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                FinalPrice = eventEntity.Price,
                QRCodeData = $"EVENT_{dto.EventId}_USER_{dto.AppUserId}_{Guid.NewGuid()}"
            });
        }

        await _uow.Orders.AddAsync(order);
        await _uow.SaveChangesAsync();

        return _mapper.Map<OrderGetDto>(order);
    }

    public async Task<OrderGetDto> GetOrderByIdAsync(Guid id)
    {
        var order = await _uow.Orders.GetSingleAsync(x => x.Id == id, "Tickets", "Tickets.Event", "Tickets.Event.Venue");

        if (order == null)
            throw new NotFoundException("Order not found.");

        return _mapper.Map<OrderGetDto>(order);
    }

    public async Task<List<OrderGetDto>> GetUserOrdersAsync(Guid userId)
    {
        var orders = await _uow.Orders
            .GetWhere(x => x.AppUserId == userId, "Tickets", "Tickets.Event")
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return _mapper.Map<List<OrderGetDto>>(orders);
    }
}
