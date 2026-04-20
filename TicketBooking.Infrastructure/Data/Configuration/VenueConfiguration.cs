using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBooking.Core.Entities;

namespace TicketBooking.Infrastructure.Data.Configuration;
public class VenueConfiguration : IEntityTypeConfiguration<Venue>
{
    public void Configure(EntityTypeBuilder<Venue> builder)
    {
        builder.Property(v => v.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(v => v.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(v => v.Events)
               .WithOne(e => e.Venue)
               .HasForeignKey(e => e.VenueId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
