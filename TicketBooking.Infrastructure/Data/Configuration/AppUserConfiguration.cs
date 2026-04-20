using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketBooking.Core.Entities;

namespace TicketBooking.Infrastructure.Data.Configuration;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Surname)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Balance)
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.HasMany(u => u.Orders)
               .WithOne(o => o.AppUser)
               .HasForeignKey(o => o.AppUserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
