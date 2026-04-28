using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Application.DTOs.Venue;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.Interfaces;
public interface IVenueService
{
    Task CreateAsync(VenueCreateDto dto);
    Task UpdateAsync(VenueUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<List<VenueGetDto>> GetAllAsync();
    Task<VenueGetDto> GetByIdAsync(Guid id);
    Task<PagedResponse<VenueGetDto>> GetVenuesPagedAsync(int page, int pageSize);
}
