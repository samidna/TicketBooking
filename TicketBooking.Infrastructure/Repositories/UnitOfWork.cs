using TicketBooking.Core.Interfaces;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    private IEventRepository _eventRepository;
    private ITicketRepository _ticketRepository;
    private ICityRepository _cityRepository;
    private IVenueRepository _venueRepository;
    private ICategoryRepository _categoryRepository;
    private IOrderRepository _orderRepository;

    public UnitOfWork(AppDbContext context) => _context = context;

    public IEventRepository Events => _eventRepository ??= new EventRepository(_context);
    public ITicketRepository Tickets => _ticketRepository ??= new TicketRepository(_context);
    public ICityRepository Cities => _cityRepository ??= new CityRepository(_context);
    public IVenueRepository Venues => _venueRepository ??= new VenueRepository(_context);
    public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(_context);
    public IOrderRepository Orders => _orderRepository ??= new OrderRepository(_context);

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
