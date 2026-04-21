using TicketBooking.Core.Entities;
using TicketBooking.Core.Interfaces;
using TicketBooking.Infrastructure.Data;

namespace TicketBooking.Infrastructure.Repositories;
public class CityRepository : GenericRepository<City>, ICityRepository
{
    public CityRepository(AppDbContext context) : base(context)
    {
    }
}
