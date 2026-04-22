using TicketBooking.Application.DTOs.Venue;

namespace TicketBooking.Application.Interfaces;
public interface IVenueService
{
    Task CreateAsync(VenueCreateDto dto);
    Task UpdateAsync(VenueUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<List<VenueGetDto>> GetAllAsync();
    Task<VenueGetDto> GetByIdAsync(Guid id);
}
