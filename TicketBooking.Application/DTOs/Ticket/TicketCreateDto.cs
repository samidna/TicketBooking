namespace TicketBooking.Application.DTOs.Ticket;
public class TicketCreateDto
{
    public Guid EventId { get; set; }
    public Guid OrderId { get; set; }
    public string? SeatNumber { get; set; }
    public decimal FinalPrice { get; set; }
}
