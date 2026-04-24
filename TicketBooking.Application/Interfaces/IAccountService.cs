using TicketBooking.Application.DTOs.User;

namespace TicketBooking.Application.Interfaces;
public interface IAccountService
{
    Task RegisterAsync(UserRegisterDto dto);
    Task<AuthResponseDto> LoginAsync(UserLoginDto dto);
}
