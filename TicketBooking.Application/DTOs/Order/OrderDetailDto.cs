using TicketBooking.Application.DTOs.Ticket;

namespace TicketBooking.Application.DTOs.Order;
public class OrderDetailDto
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public string OwnerUsername { get; set; }
    public string OwnerName { get; set; }
    public string OwnerSurname { get; set; }
    public string OwnerEmail { get; set; }
    public string OwnerPhone { get; set; }
    public List<TicketGetDto> Tickets { get; set; }
}
