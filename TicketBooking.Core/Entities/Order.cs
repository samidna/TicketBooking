using TicketBooking.Core.Enums;

namespace TicketBooking.Core.Entities;
public class Order : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
}
