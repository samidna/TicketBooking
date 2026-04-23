using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Application.DTOs.Venue;
using TicketBooking.Application.Interfaces;

namespace TicketBooking.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VenuesController : ControllerBase
{
    private readonly IVenueService _venueService;

    public VenuesController(IVenueService venueService)
    {
        _venueService = venueService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var venues = await _venueService.GetAllAsync();
        return Ok(venues);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var venue = await _venueService.GetByIdAsync(id);
        return Ok(venue);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VenueCreateDto dto)
    {
        await _venueService.CreateAsync(dto);
        return StatusCode(201, "Venue created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] VenueUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID mismatch.");

        await _venueService.UpdateAsync(dto);
        return Ok("Venue updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _venueService.DeleteAsync(id);
        return Ok("Venue deleted successfully.");
    }
}
