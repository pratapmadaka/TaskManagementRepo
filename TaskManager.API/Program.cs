using TaskManager.API.Extensions;
using TaskManager.API.Endpoints;




var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddApplicationServices();

var app = builder.Build();

// Use Middlewares
app.UseApplicationMiddlewares();

// Map Endpoints
app.MapHelloWorldEndpoint();

app.Run();
