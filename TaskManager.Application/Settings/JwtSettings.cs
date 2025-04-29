namespace TaskManager.Application.Common.Settings;

public class JwtSettings
{
    public String Secret { get; set; } = default!;
    public String Issuer { get; set; } = default!;
    public String Audience { get; set; } = default!;
    public int ExpiryMinutes { get; set; }

}
