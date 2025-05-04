using TaskManager.API.Middlewares;
using TaskManager.Application.Common.Interfaces;
using TaskManager.Application.Common.Settings;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Auth;


namespace TaskManager.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        //swagger-beginning

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "TaskManager API", Version = "v1" });

            // ✅ Add JWT Bearer Security Definition
            c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: \"Bearer eyJhbGci...\""
            });

            // ✅ Apply Bearer globally
            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
        });
        //swagger-end
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddTransient<ExceptionHandlingMiddleware>();

        //jwt service
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
