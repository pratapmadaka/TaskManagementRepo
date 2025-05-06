using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Serilog;
using TaskManager.Application.Common.Exceptions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Domain.Entities;
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


        // Sample login to return a token
        app.MapPost("/login", (IJwtTokenService jwtService) =>
        {
            var userId = Guid.NewGuid().ToString();
            var email = "admin@taskmanager.com";
            var role = "Admin"; // Could be "User" or "Manager"
            var tenantId = "Tenant1";

            var token = jwtService.GenerateToken(userId, email, role, tenantId);
            return Results.Ok(new { token });
        });

        // Protected route - only Admins
        app.MapGet("/admin", [Microsoft.AspNetCore.Authorization.Authorize(Policy = "AdminOnly")] async (ILoggerService logger) =>
        {
            await logger.LogInformation("Logged in as Admin");
            return Results.Ok("Welcome Admin! 🎉");
        });

        app.MapPost("/tasks", async (TaskItem task, ITaskRepository repo, ClaimsPrincipal user, ILoggerService _log) =>
        {
            var tenantId = user.FindFirst("tenantId")?.Value ?? "Tenant1";
            task.TenantId = tenantId;
            await _log.LogWarning($"Failed to insert task with Tenant: {task.TenantId}");
            var created = await repo.AddAsync(task);
            return Results.Ok(created);
        }).RequireAuthorization();

        app.MapGet("/tasks", async (ITaskRepository repo, ClaimsPrincipal user) =>
        {
            var tenantId = user.FindFirst("tenantId")?.Value ?? "Tenant1";
            var tasks = await repo.GetAllAsync(tenantId);
            return Results.Ok(tasks);
        }).RequireAuthorization();



    }


}
