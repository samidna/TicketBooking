using FluentValidation;
using TicketBooking.Application.DTOs.Category;

namespace TicketBooking.Application.Validations.Category;
public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name cannot be empty.")
            .MaximumLength(50).WithMessage("The category name cannot exceed 50 characters.");
    }
}
