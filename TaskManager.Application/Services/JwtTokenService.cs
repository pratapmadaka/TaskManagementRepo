using TaskManager.Application.Common.Settings;
using Microsoft.Extensions.Options;

namespace TaskManager.Application.Services;

public class JwtTokenService
{
    private readonly JwtSettings _jwtTokenService;

    public JwtTokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtTokenService = jwtSettings.Value;
    }
    public String GenerateToken()
    {
        return _jwtTokenService.Secret;
    }

}