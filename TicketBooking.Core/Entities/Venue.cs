namespace TicketBooking.Core.Entities;
public class Venue : BaseEntity
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public Guid CityId { get; set; }
    public City City { get; set; }
    public ICollection<Event> Events { get; set; }
}
