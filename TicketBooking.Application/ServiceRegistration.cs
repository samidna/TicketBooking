using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TicketBooking.Application.Interfaces;
using TicketBooking.Application.Services;

namespace TicketBooking.Application;
public static class ServiceRegistration
{
    public static void RegisterServiceForApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IVenueService, VenueService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<ITicketService, TicketService>();
    }
}
