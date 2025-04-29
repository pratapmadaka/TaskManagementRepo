using Microsoft.AspNetCore.Http.HttpResults;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Services;
namespace TaskManager.API.Endpoints;

public static class HelloWorldEndpoint
{
    public static void MapHelloWorldEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => Results.Ok("👋 TaskManager API is running!"));

        app.MapGet("/logtest", async (ILoggerService logger) =>
        {
            await logger.LogInformation("This is an info log.");
            await logger.LogWarning("This is a warning log.");
            await logger.LogError("This is an error log", new Exception("Sample Exception"));

            return Results.Ok("Logging done!");
        });

        app.MapGet("/testExcetion", (ILoggerService logger) =>
        {
            throw new NotFoundException("The task you are looking for was not found.");
        });
    }


}
