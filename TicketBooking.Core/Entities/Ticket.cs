namespace TicketBooking.Core.Entities;
public class Ticket : BaseEntity
{
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public string TicketNumber { get; set; }
    public string QRCodeData { get; set; }
    public string? SeatNumber { get; set; }
    public decimal FinalPrice { get; set; }
    public bool IsUsed { get; set; } = false;
}
