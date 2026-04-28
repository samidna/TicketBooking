using Microsoft.EntityFrameworkCore;
using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Application.DTOs.Ticket;
using TicketBooking.Application.Interfaces;
using TicketBooking.Core.Interfaces;

namespace TicketBooking.Application.Services;
public class TicketService : ITicketService
{
    private readonly IUnitOfWork _uow;

    public TicketService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<PagedResponse<TicketGetDto>> GetTicketsPagedAsync(int page, int pageSize)
    {
        var query = _uow.Tickets.GetAll();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TicketGetDto
            {
                Id = t.Id,
                TicketNumber = t.TicketNumber,
                SeatNumber = t.SeatNumber,
                FinalPrice = t.FinalPrice,
                QRCodeData = t.QRCodeData,
                IsUsed = t.IsUsed,
            })
            .ToListAsync();

        return new PagedResponse<TicketGetDto>(items, totalCount, page, pageSize);
    }
}
