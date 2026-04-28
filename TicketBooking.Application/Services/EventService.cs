using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Application.DTOs.Event;
using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Application.Exceptions;
using TicketBooking.Application.Interfaces;
using TicketBooking.Core.Entities;
using TicketBooking.Core.Interfaces;

namespace TicketBooking.Application.Services;
public class EventService : IEventService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    private readonly IFileService _fileService;
    public EventService(IMapper mapper, IUnitOfWork uow, IFileService fileService)
    {
        _mapper = mapper;
        _uow = uow;
        _fileService = fileService;
    }

    public async Task<List<EventGetDto>> GetAllAsync()
    {
        var events = await _uow.Events
            .GetAll("Category", "Venue", "Venue.City")
            .ToListAsync();

        return _mapper.Map<List<EventGetDto>>(events);
    }

    public async Task<EventGetDto> GetByIdAsync(Guid id)
    {
        var eventEntity = await _uow.Events
            .GetSingleAsync(x => x.Id == id, "Category", "Venue", "Venue.City");

        if (eventEntity == null) throw new NotFoundException("Event not found.");

        return _mapper.Map<EventGetDto>(eventEntity);
    }

    public async Task CreateAsync(EventCreateDto dto)
    {
        if (await _uow.Categories.GetByIdAsync(dto.CategoryId) == null)
            throw new NotFoundException("Category not found.");

        if (await _uow.Venues.GetByIdAsync(dto.VenueId) == null)
            throw new NotFoundException("Venue not found.");

        if (dto.EventDate <= DateTime.Now)
            throw new BadRequestException("Event date must be in the future.");

        var eventEntity = _mapper.Map<Event>(dto);

        eventEntity.ImageUrl = await _fileService.UploadFileAsync(dto.ImageFile, "events");

        await _uow.Events.AddAsync(eventEntity);
        await _uow.SaveChangesAsync();
    }

    public async Task UpdateAsync(EventUpdateDto dto)
    {
        var eventEntity = await _uow.Events.GetByIdAsync(dto.Id);
        if (eventEntity == null) throw new NotFoundException("Event not found.");

        if (eventEntity.CategoryId != dto.CategoryId)
            if (await _uow.Categories.GetByIdAsync(dto.CategoryId) == null)
                throw new NotFoundException("New Category not found.");

        if (eventEntity.VenueId != dto.VenueId)
            if (await _uow.Venues.GetByIdAsync(dto.VenueId) == null)
                throw new NotFoundException("New Venue not found.");

        if (dto.EventDate <= DateTime.Now)
            throw new BadRequestException("Updated event date must be in the future.");

        _mapper.Map(dto, eventEntity);

        if (dto.ImageFile != null)
        {
            _fileService.DeleteFile(eventEntity.ImageUrl);

            eventEntity.ImageUrl = await _fileService.UploadFileAsync(dto.ImageFile, "events");
        }

        _uow.Events.Update(eventEntity);
        await _uow.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var eventEntity = await _uow.Events.GetByIdAsync(id);
        if (eventEntity == null) throw new NotFoundException("Event not found.");

        bool hasSoldTickets = await _uow.Tickets.GetWhere(x => x.EventId == id).AnyAsync();
        if (hasSoldTickets)
            throw new BadRequestException("Cannot delete event because tickets have already been sold.");

        _uow.Events.Remove(eventEntity);
        await _uow.SaveChangesAsync();
    }

    public async Task<PagedResponse<EventGetDto>> GetEventsPagedAsync(int page, int pageSize)
    {
        var query = _uow.Events.GetAll();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(e=>new EventGetDto
            {
                Title = e.Title,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                EventDate = e.EventDate,
            })
            .ToListAsync();

        return new PagedResponse<EventGetDto>(items, totalCount, page, pageSize);
    }
}
