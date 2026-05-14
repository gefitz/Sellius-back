using Sellius.API.Middleware;

namespace Sellius.API.DI;

public static class PipelineExtension
{
    public static WebApplication UseApiPipeline(this WebApplication app)
    {
        app.UseCors("CorsPolicy");
        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
