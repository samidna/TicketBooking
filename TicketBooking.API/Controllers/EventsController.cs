using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Application.DTOs.Event;
using TicketBooking.Application.Interfaces;

namespace TicketBooking.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventService.GetAllAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var eventEntity = await _eventService.GetByIdAsync(id);
        return Ok(eventEntity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] EventCreateDto dto)
    {
        await _eventService.CreateAsync(dto);
        return StatusCode(201, "Event created successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromForm] EventUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID mismatch.");

        await _eventService.UpdateAsync(dto);
        return Ok("Event updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _eventService.DeleteAsync(id);
        return Ok("Event deleted successfully.");
    }
}
