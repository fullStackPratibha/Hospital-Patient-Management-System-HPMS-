using System.Text.Json;
using HospitalManagementAPI.Exceptions;
using HospitalManagementAPI.Response;
using Microsoft.Extensions.Logging;

namespace HospitalManagementAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }catch(Exception ex)
        {
            _logger.LogError(
    ex,
    "Unhandled Exception occurred.\nPath: {Path}\nTraceId: {TraceId}",
    context.Request.Path,
    context.TraceIdentifier);

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
                case UnauthorizedAccessException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                case InvalidCredentialException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                case ArgumentException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            var response = new
            {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message,
                    Path = context.Request.Path,
                    TraceId = context.TraceIdentifier,
                    TimeStamp = DateTime.UtcNow,
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}