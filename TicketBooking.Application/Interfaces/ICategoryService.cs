using TicketBooking.Application.DTOs.Category;

namespace TicketBooking.Application.Interfaces;
public interface ICategoryService
{
    Task<List<CategoryGetDto>> GetAllAsync();
    Task<CategoryGetDto> GetByIdAsync(Guid id);
    Task CreateAsync(CategoryCreateDto dto);
    Task UpdateAsync(Guid id, CategoryCreateDto dto);
    Task DeleteAsync(Guid id);
}
