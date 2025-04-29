namespace TaskManager.API.Extensions;

using Serilog;

public static class LoggingExtensions
{
    public static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration().
        ReadFrom.Configuration(builder.Configuration).
        Enrich.FromLogContext().
        WriteTo.Console().
        WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day).
        CreateLogger();

        builder.Host.UseSerilog();
        return builder;
    }
}