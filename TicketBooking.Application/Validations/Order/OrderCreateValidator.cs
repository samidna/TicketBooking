using FluentValidation;
using TicketBooking.Application.DTOs.Order;

namespace TicketBooking.Application.Validations.Order;
public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateValidator()
    {
        RuleFor(x => x.TicketCount)
            .InclusiveBetween(1, 10).WithMessage("You can buy a minimum of 1 and a maximum of 10 tickets at a time.");
        RuleFor(x => x.EventId).NotEmpty();
        RuleFor(x => x.AppUserId).NotEmpty();
    }
}
