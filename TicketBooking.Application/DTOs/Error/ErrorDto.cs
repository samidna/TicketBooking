using System.Text.Json;

namespace TicketBooking.Application.DTOs.Error;
public class ErrorDto
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string? Detail { get; set; } 

    public override string ToString() => JsonSerializer.Serialize(this);
}
