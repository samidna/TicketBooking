using Microsoft.Extensions.Logging;

namespace TicketBooking.Core.Entities;
public class Category : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Event> Events { get; set; }
}
