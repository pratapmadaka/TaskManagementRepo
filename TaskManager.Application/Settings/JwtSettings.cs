namespace TaskManager.Application.Common.Settings;

public class JwtSettings
{
    public String Secret { get; set; } = String.Empty;
    public String Issuer { get; set; } = String.Empty;
    public String Audience { get; set; } = String.Empty;
    public int ExpiryMinutes { get; set; }

}
