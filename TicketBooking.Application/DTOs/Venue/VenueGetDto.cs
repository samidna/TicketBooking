namespace TicketBooking.Application.DTOs.Venue;
public class VenueGetDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public string CityName { get; set; }
}
