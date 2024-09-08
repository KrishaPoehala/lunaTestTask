using LunaTestTask.Application.Common.Exceptions;

namespace LunaTestTask.Middlewares;

public class ErrorHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next(context);
		}
		catch(FluentValidation.ValidationException ex)
		{
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
            });
        }
        catch (Exception ex)
		{
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status400BadRequest,
            };

            await context.Response.WriteAsJsonAsync(new
            {
                ex.Message
            });

        }
    }
}
