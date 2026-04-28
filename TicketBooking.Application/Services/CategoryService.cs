using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Application.DTOs.Category;
using TicketBooking.Application.DTOs.Pagination;
using TicketBooking.Application.Exceptions;
using TicketBooking.Application.Interfaces;
using TicketBooking.Core.Entities;
using TicketBooking.Core.Interfaces;

namespace TicketBooking.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _uow.Categories.GetAll().ToListAsync();
        return _mapper.Map<List<CategoryGetDto>>(categories);
    }

    public async Task<CategoryGetDto> GetByIdAsync(Guid id)
    {
        var category = await _uow.Categories.GetByIdAsync(id);
        if (category == null) throw new NotFoundException("Category not found.");
        return _mapper.Map<CategoryGetDto>(category);
    }

    public async Task CreateAsync(CategoryCreateDto dto)
    {
        var exist = await _uow.Categories.GetWhere(c => c.Name.ToLower() == dto.Name.Trim().ToLower()).AnyAsync();

        if (exist)
            throw new AlreadyExistsException("This category already exists.");

        var category = _mapper.Map<Category>(dto);
        await _uow.Categories.AddAsync(category);
        await _uow.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, CategoryUpdateDto dto)
    {
        var category = await _uow.Categories.GetByIdAsync(id);
        if (category == null) throw new NotFoundException("Category not found.");

        if (category.Name.ToLower() != dto.Name.ToLower())
        {
            bool exists = await _uow.Categories.GetWhere(c => c.Name.ToLower() == dto.Name.Trim().ToLower() && c.Id != category.Id)
                .AnyAsync();

            if (exists)
                throw new AlreadyExistsException("This category name already used");
        }

        _mapper.Map(dto, category);
        _uow.Categories.Update(category);
        await _uow.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _uow.Categories.GetByIdAsync(id);
        if (category == null) throw new NotFoundException("Category not found.");

        bool hasEvents = await _uow.Events.GetWhere(x => x.CategoryId == id).AnyAsync();
        if (hasEvents)
            throw new BadRequestException("Cannot delete because there are events associated with this category..");

        _uow.Categories.Remove(category);
        await _uow.SaveChangesAsync();
    }

    public async Task<PagedResponse<CategoryGetDto>> GetCategoriesPagedAsync(int page, int pageSize)
    {
        var query = _uow.Categories.GetAll();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(e => e.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CategoryGetDto
            {
                Name = c.Name
            })
            .ToListAsync();

        return new PagedResponse<CategoryGetDto>(items, totalCount, page, pageSize);
    }
}
