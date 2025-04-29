using TaskManager.API.Middlewares;

namespace TaskManager.API.Extensions;

public static class AppExtensions
{
    public static WebApplication UseApplicationMiddlewares(this WebApplication app)
    {



        if (app.Environment.IsDevelopment())
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        return app;
    }
}
