using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBooking.Core.Entities;

namespace TicketBooking.Infrastructure.Data.Configuration;
public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.HasMany(c => c.Venues)
               .WithOne(v => v.City)
               .HasForeignKey(v => v.CityId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
