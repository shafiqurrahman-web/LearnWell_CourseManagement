using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Dapper;
using LearnWell.CourseManagement.Application.Abstractions.Clock;
using LearnWell.CourseManagement.Application.Abstractions.Data;
using LearnWell.CourseManagement.Application.Abstractions.Email;
using LearnWell.CourseManagement.Domain.Entities.Abstractions;
using LearnWell.CourseManagement.Domain.Entities.Courses;
using LearnWell.CourseManagement.Domain.Entities.Users;
using LearnWell.CourseManagement.Infrastructure.Clock;
using LearnWell.CourseManagement.Infrastructure.Data;
using LearnWell.CourseManagement.Infrastructure.Email;
using LearnWell.CourseManagement.Infrastructure.Outbox;
using LearnWell.CourseManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Quartz;
using AuthenticationOptions = LearnWell.CourseManagement.Infrastructure.Authentication.AuthenticationOptions;
using AuthenticationService = LearnWell.CourseManagement.Infrastructure.Authentication.AuthenticationService;
using IAuthenticationService = LearnWell.CourseManagement.Application.Abstractions.Authentication.IAuthenticationService;
using LearnWell.CourseManagement.Infrastructure.Authentication;
using LearnWell.CourseManagement.Application.Abstractions.Authentication;
using LearnWell.CourseManagement.Infrastructure.Authorization;
using LearnWell.CourseManagement.Application.Abstractions.Caching;
using LearnWell.CourseManagement.Infrastructure.Caching;
using Asp.Versioning;

namespace LearnWell.CourseManagement.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        AddPersistence(services, configuration);

       

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
                var connectionString = configuration.GetConnectionString("LearnWellDatabase") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        #region Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        

        #endregion

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }

    
}
