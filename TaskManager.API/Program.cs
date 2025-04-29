using TaskManager.API.Extensions;
using TaskManager.API.Endpoints;
using System.Net;




var builder = WebApplication.CreateBuilder(args);



// Add Serilog Logging
builder.AddSerilogLogging();
// Add Services
builder.Services.AddApplicationServices();
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5021); // Ensures the app listens on port 5021
});

var app = builder.Build();

// Use Middlewares
app.UseApplicationMiddlewares();

// Map Endpoints
app.MapHelloWorldEndpoint();

app.Run();
