
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.DTOs.Auth;
using TaskManager.Domain.Entities;

namespace TaskManager.API.Endpoints;

public static class RegisterNLoginEndPoints
{
    public static void MapRegisterNLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (RegisterRequest request, IUserRepository userRepository, IJwtTokenService jwtTokenService) =>
        {
            var existingUser = userRepository.GetByEmailAsync(request.Email);
            if (existingUser.Result is not null) return Results.BadRequest("Email already Exists");

            var user = new User
            {
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                TenentID = request.TenentID,
                FullName = request.FullName
            };
            await userRepository.AddUserAsync(user);
            var jwtToken = jwtTokenService.GenerateToken(user.Id, user.Email, user.Role, user.TenentID);
            return Results.Ok(new { jwtToken });
        });


        app.MapPost("/auth/login/", async (LoginRequest loginRequest, IUserRepository userRepository, IJwtTokenService jwtTokenService) =>
        {
            var user = await userRepository.GetByEmailAsync(loginRequest.Email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
                return Results.Unauthorized();
            var token = jwtTokenService.GenerateToken(user.Id, user.Email, user.Role, user.TenentID);
            return Results.Ok(new { token });
        });
    }
}
