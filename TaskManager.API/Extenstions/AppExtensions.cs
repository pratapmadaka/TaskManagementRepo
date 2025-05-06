using System.Threading.Tasks;
using TaskManager.API.Middlewares;

namespace TaskManager.API.Extensions;

public static class AppExtensions
{
    public static async Task<WebApplication> UseApplicationMiddlewares(this WebApplication app)
    {



        if (app.Environment.IsDevelopment())
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        var cosmosService = app.Services.GetRequiredService<ICosmosDbService>();
        await cosmosService.InitializeAsync();
        app.UseHttpsRedirection();

        //authorization
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
