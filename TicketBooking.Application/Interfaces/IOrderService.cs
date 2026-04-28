using TicketBooking.Application.DTOs.Order;
using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.Interfaces;
public interface IOrderService
{
    Task<OrderGetDto> CreateOrderAsync(OrderCreateDto dto);
    Task<List<OrderGetDto>> GetUserOrdersAsync(Guid userId);
    Task<OrderGetDto> GetOrderByIdAsync(Guid id);
    Task<PagedResponse<OrderGetDto>> GetOrdersPagedAsync(int page, int pageSize);
}
