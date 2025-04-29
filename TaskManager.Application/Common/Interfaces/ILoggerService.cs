namespace TaskManager.Application.Common.Interfaces
{
    public interface ILoggerService
    {
        Task LogInformation(string message);
        Task LogWarning(string message);
        Task LogError(string message, Exception exception);
    }
}