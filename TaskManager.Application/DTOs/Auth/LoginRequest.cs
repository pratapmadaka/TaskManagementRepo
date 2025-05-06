namespace TaskManager.Application.DTOs.Auth;

public record LoginRequest(String Email, String Password, string TenantId);