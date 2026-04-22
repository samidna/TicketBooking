using TicketBooking.Application.DTOs.City;

namespace TicketBooking.Application.Interfaces;
public interface ICityService
{
    Task CreateAsync(CityCreateDto dto);
    Task UpdateAsync(CityUpdateDto dto);
    Task DeleteAsync(Guid id);
    Task<List<CityGetDto>> GetAllAsync();
    Task<CityGetDto> GetByIdAsync(Guid id);
}
