using TicketBooking.Application.DTOs.Order;

namespace TicketBooking.Application.Interfaces;
public interface IOrderService
{
    Task<OrderGetDto> CreateOrderAsync(OrderCreateDto dto);
    Task<List<OrderGetDto>> GetUserOrdersAsync(Guid userId);
    Task<OrderGetDto> GetOrderByIdAsync(Guid id);
}
