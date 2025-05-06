using TaskManager.API.Extensions;
using TaskManager.API.Endpoints;
using System.Net;
using TaskManager.Application.Common.Settings;
//jwt
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;




var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<CosmosDbSettings>(builder.Configuration.GetSection("CosmosDb"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);


//authentication
builder.Services.AddAuthentication(Options =>
{
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(Options =>
{
    Options.RequireHttpsMetadata = false;
    Options.SaveToken = true;
    Options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

// Authorization
builder.Services.AddAuthorizationBuilder()
                    // Authorization
                    .AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));


// Add Serilog Logging
builder.AddSerilogLogging();


// Add Services
builder.Services.AddApplicationServices();
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5021); // Ensures the app listens on port 5021
});


//middleware Pipeline
var app = builder.Build();

// Use Middlewares
await app.UseApplicationMiddlewares();


//auth
app.UseAuthentication(); // 👈 BEFORE Authorization
app.UseAuthorization();

// Map Endpoints
app.MapHelloWorldEndpoint();
app.MapRegisterNLoginEndpoint();

app.Run();
