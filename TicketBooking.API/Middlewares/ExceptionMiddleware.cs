using System.Net;
using TicketBooking.Application.DTOs.Error;
using TicketBooking.Application.Exceptions;

namespace TicketBooking.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var error = new ErrorDto
        {
            Message = exception.Message, 
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        switch (exception)
        {
            case NotFoundException:
                error.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case AlreadyExistsException:
                error.StatusCode = (int)HttpStatusCode.Conflict; 
                break;
            case BadRequestException:
                error.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case UnauthorizedAccessException:
                error.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            default:
                error.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.StatusCode = error.StatusCode;
        await context.Response.WriteAsync(error.ToString());
    }
}
