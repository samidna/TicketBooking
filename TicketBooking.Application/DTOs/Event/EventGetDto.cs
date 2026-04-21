namespace TicketBooking.Application.DTOs.Event;
public class EventGetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime EventDate { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; }
    public string CityName { get; set; }
    public string VenueName { get; set; }
}
