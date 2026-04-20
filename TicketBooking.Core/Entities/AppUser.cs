using Microsoft.AspNetCore.Identity;

namespace TicketBooking.Core.Entities;
public class AppUser : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public decimal Balance { get; set; }
    public ICollection<Order> Orders { get; set; }
}
