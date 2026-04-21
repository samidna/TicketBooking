using FluentValidation;
using TicketBooking.Application.DTOs.Event;

namespace TicketBooking.Application.Validations.Event;
public class EventCreateValidator : AbstractValidator<EventCreateDto>
{
    public EventCreateValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title can not be empty.");
        RuleFor(x => x.Price).InclusiveBetween(1, 10000).WithMessage("The value must be between 1-10000.");
        RuleFor(x => x.EventDate)
            .GreaterThan(DateTime.Now).WithMessage("The event date cannot be past.");
        RuleFor(x => x.ImageFile).NotNull().WithMessage("Image fiel can not be empty.");
        RuleFor(x => x.ImageFile.Length).LessThanOrEqualTo(2 * 1024 * 1024)
            .WithMessage("Image can not be more than 2 MB.");
    }
}
