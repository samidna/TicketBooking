using TicketBooking.Application.DTOs.Event;
using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.Interfaces;
public interface IEventService
{
    Task<List<EventGetDto>> GetAllAsync();
    Task<EventGetDto> GetByIdAsync(Guid id);
    Task CreateAsync(EventCreateDto dto);
    Task UpdateAsync(EventUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<PagedResponse<EventGetDto>> GetEventsPagedAsync(int page, int pageSize);
}
