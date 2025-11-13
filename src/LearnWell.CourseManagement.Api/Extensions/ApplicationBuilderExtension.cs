using LearnWell.CourseManagement.Api.Middlewares;
using LearnWell.CourseManagement.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace LearnWell.CourseManagement.Api.Extensions;

public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Run EF Migrations only for internal development - DEV
    /// </summary>
    /// <param name="app"></param>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        ApplyMigration<ApplicationDbContext>(scope);       
    }

    private static void ApplyMigration<TDBContext>(IServiceScope scope)
       where TDBContext : DbContext
    {
        using TDBContext dBContext = scope.ServiceProvider.GetRequiredService<TDBContext>();
        if (dBContext.Database.IsNpgsql())
        {
            dBContext.Database.Migrate();
        }
    }




    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlingMiddleware>();

    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}
