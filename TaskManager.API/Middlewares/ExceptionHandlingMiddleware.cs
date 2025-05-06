using System.Net;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.API.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private ILoggerService _logger;
    private IWebHostEnvironment _env;

    public ExceptionHandlingMiddleware(ILoggerService logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await _logger.LogError("Unhadleed Error Exception", ex);


            var StatusCode = ex switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                UnauthorizedException => HttpStatusCode.Unauthorized,
                ValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)StatusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = "Unhandeled Error Exception",
                errorType = ex.GetType().Name,
                stackTrace = _env.IsDevelopment() ? ex.StackTrace : null
            };
            await context.Response.WriteAsJsonAsync(response);

        }
    }

}