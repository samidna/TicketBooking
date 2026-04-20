using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBooking.Core.Entities;

namespace TicketBooking.Infrastructure.Data.Configuration;
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.Property(t => t.TicketNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(t => t.TicketNumber)
            .IsUnique(); 

        builder.Property(t => t.QRCodeData)
            .IsRequired();

        builder.Property(t => t.FinalPrice)
            .HasPrecision(18, 2);

        builder.Property(t => t.SeatNumber)
            .HasMaxLength(10);
    }
}
