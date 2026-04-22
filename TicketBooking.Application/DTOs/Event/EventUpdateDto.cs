using Microsoft.AspNetCore.Http;

namespace TicketBooking.Application.DTOs.Event;
public class EventUpdateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile? ImageFile { get; set; }
    public DateTime EventDate { get; set; }
    public decimal Price { get; set; }
    public Guid VenueId { get; set; }
    public Guid CategoryId { get; set; }
}
