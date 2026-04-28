using TicketBooking.Application.DTOs.City;
using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.Interfaces;
public interface ICityService
{
    Task CreateAsync(CityCreateDto dto);
    Task UpdateAsync(CityUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<List<CityGetDto>> GetAllAsync();
    Task<CityGetDto> GetByIdAsync(Guid id);
    Task<PagedResponse<CityGetDto>> GetCitiesPagedAsync(int page, int pageSize);
}
