namespace TicketBooking.Application.DTOs.Order;
public class OrderGetDto
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public string OwnerUsername { get; set; }
}
