namespace TicketBooking.Application.DTOs.Ticket;
public class TicketDetailDto
{
    public Guid Id { get; set; }
    public string TicketNumber { get; set; }
    public string EventTitle { get; set; }
    public string VenueName { get; set; }
    public string? SeatNumber { get; set; }
    public decimal FinalPrice { get; set; }
    public string QRCodeData { get; set; }
    public bool IsUsed { get; set; }
}
