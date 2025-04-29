namespace TaskManager.API.Extensions;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton<ILoggerService, LoggerService>();

        return services;
    }
}
