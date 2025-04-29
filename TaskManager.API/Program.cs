using TaskManager.API.Extensions;
using TaskManager.API.Endpoints;
using System.Net;
using TaskManager.Application.Common.Settings;




var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<CosmosDb>(builder.Configuration.GetSection("CosmosDb"));

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
app.UseApplicationMiddlewares();

// Map Endpoints
app.MapHelloWorldEndpoint();

app.Run();
