namespace TicketBooking.Application.DTOs.Venue;
public class VenueCreateDto
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public Guid CityId { get; set; }
}
