using TicketBooking.Core.Entities;

namespace TicketBooking.Core.Interfaces;
public interface ITokenService
{
    string CreateToken(AppUser user, IList<string> roles);
}
