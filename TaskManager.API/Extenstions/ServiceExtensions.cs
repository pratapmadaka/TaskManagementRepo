namespace TaskManager.API.Extensions;

using TaskManager.API.Middlewares;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Settings;
using TaskManager.Application.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddTransient<ExceptionHandlingMiddleware>();

        return services;
    }
}
