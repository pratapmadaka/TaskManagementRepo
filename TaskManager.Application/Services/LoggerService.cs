using TaskManager.Application.Common.Interfaces;
using Serilog;

namespace TaskManager.Application.Services;

public class LoggerService : ILoggerService
{
    private ILogger? _logger;

    public LoggerService()
    {

    }
    private ILogger Logger => _logger ??= Log.ForContext<LoggerService>();
    public async Task LogInformation(string message)
    {
        if (_logger == null) _logger = Log.ForContext<LoggerService>();
        await Task.Run(() => _logger.Information(message));
    }
    public async Task LogWarning(string message)
    {
        if (_logger == null) _logger = Log.ForContext<LoggerService>();
        await Task.Run(() => _logger.Warning(message));
    }
    public async Task LogError(string message, Exception ex)
    {
        if (_logger == null) _logger = Log.ForContext<LoggerService>();
        await Task.Run(() => _logger.Error(ex, message));
    }

}

