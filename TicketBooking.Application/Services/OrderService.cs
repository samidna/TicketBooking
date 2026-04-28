using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Application.DTOs.Order;
using TicketBooking.Application.DTOs.Pagination;
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
    private readonly UserManager<AppUser> _userManager;

    public OrderService(IMapper mapper, IUnitOfWork uow, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _uow = uow;
        _userManager = userManager;
    }

    public async Task<OrderGetDto> CreateOrderAsync(OrderCreateDto dto)
    {
        await _uow.BeginTransactionAsync();

        try
        {
            var eventEntity = await _uow.Events.GetSingleAsync(x => x.Id == dto.EventId, "Venue");
            if (eventEntity == null) throw new NotFoundException("Event not found.");

            int soldTicketsCount = await _uow.Tickets.GetWhere(x => x.EventId == dto.EventId).CountAsync();
            if (soldTicketsCount + dto.TicketCount > eventEntity.Venue.Capacity)
                throw new BadRequestException($"Only {eventEntity.Venue.Capacity - soldTicketsCount} tickets available.");

            var user = await _userManager.FindByIdAsync(dto.AppUserId.ToString());
            if (user == null) throw new NotFoundException("User not found.");

            decimal totalAmount = eventEntity.Price * dto.TicketCount;
            if (user.Balance < totalAmount)
                throw new BadRequestException("Insufficient balance.");

            user.Balance -= totalAmount;

            var order = new Order
            {
                AppUserId = dto.AppUserId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Completed,
                TotalAmount = totalAmount,
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

            await _uow.CommitTransactionAsync();

            return _mapper.Map<OrderGetDto>(order);
        }
        catch (Exception)
        {
            await _uow.RollbackTransactionAsync();
            throw;
        }
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

    public async Task<PagedResponse<OrderGetDto>> GetOrdersPagedAsync(int page, int pageSize)
    {
        var query = _uow.Orders.GetAll();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new OrderGetDto
            {
                Id = o.Id,
                OrderDate = o.CreatedAt,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                OwnerUsername = o.AppUser.UserName,
            })
            .ToListAsync();

        return new PagedResponse<OrderGetDto>(items, totalCount, page, pageSize);
    }
}
