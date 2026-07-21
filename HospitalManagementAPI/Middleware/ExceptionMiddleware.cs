using System.Text.Json;
using HospitalManagementAPI.Exceptions;

namespace HospitalManagementAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }catch(Exception ex)
        {
            context.Response.ContentType = "application/json";

            switch(ex)
            {
                case DuplicateEmailException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;
                case DuplicatePhoneException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;
                case PatientNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var response = new
            {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                    InnerException = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}