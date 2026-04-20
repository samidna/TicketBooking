using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBooking.Core.Entities;

namespace TicketBooking.Infrastructure.Data.Configuration;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.TotalAmount).HasPrecision(18, 2);

        builder.HasMany(o => o.Tickets)
               .WithOne(t => t.Order)
               .HasForeignKey(t => t.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
