using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Application.DTOs.City;
using TicketBooking.Application.DTOs.Venue;
using TicketBooking.Application.Exceptions;
using TicketBooking.Application.Interfaces;
using TicketBooking.Core.Entities;
using TicketBooking.Core.Interfaces;

namespace TicketBooking.Application.Services;
public class VenueService : IVenueService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public VenueService(IMapper mapper, IUnitOfWork uow)
    {
        _mapper = mapper;
        _uow = uow;
    }

    public async Task CreateAsync(VenueCreateDto dto)
    {
        var cityExists = await _uow.Cities.GetByIdAsync(dto.CityId) != null;
        if (!cityExists)
            throw new NotFoundException("The specified city does not exist.");

        bool exists = await _uow.Venues
            .GetWhere(x => x.Name.ToLower() == dto.Name.Trim().ToLower() && x.CityId == dto.CityId)
            .AnyAsync();

        if (exists)
            throw new AlreadyExistsException("A venue with this name already exists in this city.");

        var venue = _mapper.Map<Venue>(dto);
        await _uow.Venues.AddAsync(venue);
        await _uow.SaveChangesAsync();
    }

    public async Task UpdateAsync(VenueUpdateDto dto)
    {
        var venue = await _uow.Venues.GetByIdAsync(dto.Id);
        if (venue == null)
            throw new NotFoundException("Venue not found.");

        if (venue.CityId != dto.CityId)
        {
            var cityExists = await _uow.Cities.GetByIdAsync(dto.CityId) != null;
            if (!cityExists)
                throw new NotFoundException("The specified new City does not exist.");
        }

        if (venue.Name.ToLower() != dto.Name.Trim().ToLower() || venue.CityId != dto.CityId)
        {
            bool exists = await _uow.Venues
                .GetWhere(x => x.Name.ToLower() == dto.Name.Trim().ToLower()
                            && x.CityId == dto.CityId
                            && x.Id != dto.Id) 
                .AnyAsync();

            if (exists)
                throw new BadRequestException("Another venue with this name already exists in the selected city.");
        }

        _mapper.Map(dto, venue);
        _uow.Venues.Update(venue);
        await _uow.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var venue = await _uow.Venues.GetByIdAsync(id);
        if (venue == null)
            throw new NotFoundException("Venue not found.");

        bool hasEvents = await _uow.Events.GetWhere(x => x.VenueId == id).AnyAsync();
        if (hasEvents)
            throw new BadRequestException("Cannot delete venue because there are events scheduled here.");

        _uow.Venues.Remove(venue);
        await _uow.SaveChangesAsync();
    }

    public async Task<List<VenueGetDto>> GetAllAsync()
    {
        var venues = await _uow.Venues.GetAll("City").ToListAsync();
        return _mapper.Map<List<VenueGetDto>>(venues);
    }

    public async Task<VenueGetDto> GetByIdAsync(Guid id)
    {
        var venue = await _uow.Venues.GetByIdAsync(id);
        if (venue == null) throw new NotFoundException("Venue not found.");
        return _mapper.Map<VenueGetDto>(venue);
    }
}
