using FluentValidation;
using TicketBooking.Application.DTOs.City;

namespace TicketBooking.Application.Validations.City;
public class CityCreateValidator : AbstractValidator<CityCreateDto>
{
    public CityCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("City name cannot be empty.")
            .MaximumLength(100).WithMessage("The city name cannot exceed 100 characters.");
    }
}
