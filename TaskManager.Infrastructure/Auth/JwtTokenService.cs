using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Settings;

namespace TaskManager.Infrastructure.Auth;
public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;
    public JwtTokenService(IOptions<JwtSettings> jwtoptions)
    {
        _jwtSettings = jwtoptions.Value;
    }
    public string GenerateToken(string userId, string email, string role, string tenantId)
    {
        var Claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,userId),
            new Claim(JwtRegisteredClaimNames.Email,email),
            new Claim(ClaimTypes.Role,role),
            new Claim("tenentId", tenantId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            Claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}