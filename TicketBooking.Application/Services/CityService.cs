using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Application.DTOs.City;
using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Application.Exceptions;
using TicketBooking.Application.Interfaces;
using TicketBooking.Core.Entities;
using TicketBooking.Core.Interfaces;

namespace TicketBooking.Application.Services;
public class CityService : ICityService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public CityService(IMapper mapper, IUnitOfWork uow)
    {
        _mapper = mapper;
        _uow = uow;
    }

    public async Task CreateAsync(CityCreateDto dto)
    {
        bool exists = await _uow.Cities
            .GetWhere(x => x.Name.ToLower() == dto.Name.ToLower())
            .AnyAsync();

        if (exists)
            throw new AlreadyExistsException("City with this name already exists.");

        var city = _mapper.Map<City>(dto);
        await _uow.Cities.AddAsync(city);
        await _uow.SaveChangesAsync();
    }

    public async Task UpdateAsync(CityUpdateDto dto)
    {
        var city = await _uow.Cities.GetByIdAsync(dto.Id);
        if (city == null)
            throw new NotFoundException("City not found.");

        if (city.Name.ToLower() != dto.Name.ToLower())
        {
            bool nameExists = await _uow.Cities
                .GetWhere(x => x.Name.ToLower() == dto.Name.ToLower() && x.Id != dto.Id)
                .AnyAsync();

            if (nameExists)
                throw new AlreadyExistsException("Another city with this name already exists.");
        }

        _mapper.Map(dto, city);
        _uow.Cities.Update(city);
        await _uow.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var city = await _uow.Cities.GetByIdAsync(id);
        if (city == null)
            throw new NotFoundException("City not found.");

        bool hasVenues = await _uow.Venues.GetWhere(x => x.CityId == id).AnyAsync();
        if (hasVenues)
            throw new BadRequestException("Cannot delete city because it has registered venues.");

        _uow.Cities.Remove(city);
        await _uow.SaveChangesAsync();
    }

    public async Task<List<CityGetDto>> GetAllAsync()
    {
        var cities = await _uow.Cities.GetAll().ToListAsync();
        return _mapper.Map<List<CityGetDto>>(cities);
    }

    public async Task<CityGetDto> GetByIdAsync(Guid id)
    {
        var city = await _uow.Cities.GetByIdAsync(id);
        if (city == null) throw new NotFoundException("City not found.");
        return _mapper.Map<CityGetDto>(city);
    }

    public async Task<PagedResponse<CityGetDto>> GetCitiesPagedAsync(int page, int pageSize)
    {
        var query = _uow.Cities.GetAll();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c=>new CityGetDto
            {
                Name = c.Name
            })
            .ToListAsync();

        return new PagedResponse<CityGetDto>(items, totalCount, page, pageSize);
    }
}
