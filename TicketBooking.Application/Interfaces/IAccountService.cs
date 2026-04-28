using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Application.DTOs.User;
using TicketBooking.Core.Entities;

namespace TicketBooking.Application.Interfaces;
public interface IAccountService
{
    Task RegisterAsync(UserRegisterDto dto);
    Task<AuthResponseDto> LoginAsync(UserLoginDto dto);
    Task<PagedResponse<UserGetDto>> GetUsersPagedAsync(int page, int pageSize);
}
