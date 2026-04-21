using TicketBooking.Core.Entities;
using TicketBooking.Core.Interfaces;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.Infrastructure.Repositories;
public class VenueRepository : GenericRepository<Venue>, IVenueRepository
{
    public VenueRepository(AppDbContext context) : base(context)
    {
    }
}
