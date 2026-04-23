using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Application.DTOs.City;
using TicketBooking.Application.Interfaces;

namespace TicketBooking.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cities = await _cityService.GetAllAsync();
        return Ok(cities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var city = await _cityService.GetByIdAsync(id);
        return Ok(city);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CityCreateDto dto)
    {
        await _cityService.CreateAsync(dto);
        return StatusCode(201, "City added successfully.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CityUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest("ID mismatch.");
        await _cityService.UpdateAsync(dto);
        return Ok("City updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _cityService.DeleteAsync(id);
        return Ok("City deleted successfully.");
    }
}
