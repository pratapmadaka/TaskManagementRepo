namespace TaskManager.API.Endpoints;

public static class HelloWorldEndpoint
{
    public static void MapHelloWorldEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => Results.Ok("👋 TaskManager API is running!"));
    }
}
