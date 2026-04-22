using FluentValidation;
using TicketBooking.Application.DTOs.Venue;

namespace TicketBooking.Application.Validations.Venue;
public class VenueUpdateValidator : AbstractValidator<VenueUpdateDto>
{
    public VenueUpdateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("The location name can not be empty");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Address can not be empty.");
        RuleFor(x => x.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0.");
        RuleFor(x => x.CityId).NotEmpty().WithMessage("A city must be selected.");
    }
}
