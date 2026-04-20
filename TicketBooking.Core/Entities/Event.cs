using System.Net.Sockets;

namespace TicketBooking.Core.Entities;
public class Event : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime EventDate { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Guid VenueId { get; set; }
    public Venue Venue { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}
