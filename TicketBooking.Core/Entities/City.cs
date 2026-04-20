namespace TicketBooking.Core.Entities;
public class City : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Venue> Venues { get; set; }
}
