namespace TicketBooking.Application.DTOs.Order;
public class OrderCreateDto
{
    public Guid AppUserId { get; set; }
    public Guid EventId { get; set; }
    public int TicketCount { get; set; }
}
