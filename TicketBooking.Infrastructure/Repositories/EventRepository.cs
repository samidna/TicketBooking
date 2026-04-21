using TicketBooking.Core.Entities;
using TicketBooking.Core.Interfaces;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.Infrastructure.Repositories;
public class EventRepository : GenericRepository<Event>, IEventRepository
{
    public EventRepository(AppDbContext context) : base(context)
    {
    }
}
