namespace TicketBooking.Core.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IEventRepository Events { get; }
    ITicketRepository Tickets { get; }
    ICityRepository Cities { get; }
    IVenueRepository Venues { get; }
    ICategoryRepository Categories { get; }
    IOrderRepository Orders { get; }

    Task<int> SaveChangesAsync();
}
