using TicketBooking.Application.DTOs.Event;

namespace TicketBooking.Application.Interfaces;
public interface IEventService
{
    Task<List<EventGetDto>> GetAllAsync();
    Task<EventGetDto> GetByIdAsync(Guid id);
    Task CreateAsync(EventCreateDto dto);
    Task UpdateAsync(EventUpdateDto dto);
    Task DeleteAsync(Guid id);
}
