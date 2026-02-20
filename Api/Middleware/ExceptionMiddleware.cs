using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Api.Middleware; 

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context); 
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception was caught by the Global Exception Middleware.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var statusCode = (int)HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred while processing your request.";
        
        object errorResponse;

        switch (exception)
        {
            case UnauthorizedAccessException:
                statusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse = new { Success = false, Msg = exception.Message };
                break;
        
            case KeyNotFoundException:
                statusCode = (int)HttpStatusCode.NotFound;
                errorResponse = new { Success = false, Msg = exception.Message };
                break;
        
            case ValidationException validationException:
                statusCode = (int)HttpStatusCode.BadRequest;
                var errors = validationException.Errors.Select(e => e.ErrorMessage);
                var combinedMessages = string.Join(" ", errors);
        
                errorResponse = new { Success = false, Message = combinedMessages };
                break;
        
            default:
                errorResponse = new { Success = false, Message = message };
                break;
        }

        context.Response.StatusCode = statusCode;
        var jsonResult = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        await context.Response.WriteAsync(jsonResult);
    }
}