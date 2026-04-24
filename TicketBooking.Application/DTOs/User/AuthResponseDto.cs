namespace TicketBooking.Application.DTOs.User;
public class AuthResponseDto
{
    public string Token { get; set; }
    public string Username { get; set; }
    public DateTime Expiration { get; set; }
}
