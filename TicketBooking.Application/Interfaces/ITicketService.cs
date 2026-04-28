using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Application.DTOs.Ticket;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.Interfaces;
public interface ITicketService
{
    Task<PagedResponse<TicketGetDto>> GetTicketsPagedAsync(int page, int pageSize);
}
