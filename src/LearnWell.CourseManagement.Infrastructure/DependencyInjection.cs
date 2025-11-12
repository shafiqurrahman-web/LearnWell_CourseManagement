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
using LearnWell.CourseManagement.Infrastructure.Authorization.Constants;
using LearnWell.CourseManagement.Infrastructure.Ddatabase;


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
        AddCaching(services, configuration);
        AddBackgroundJobs(services, configuration);
        AddApiVersioning(services);

        AddAuthentication(services, configuration);
        AddAuthorization(services);
        AddHealthChecks(services, configuration);

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

    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        services.AddQuartz(configurator =>
        {
            var schedulerId = Guid.NewGuid();
            configurator.SchedulerId = $"default-id-{schedulerId}";
            configurator.SchedulerName = $"default-name-{schedulerId}";
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessOutboxMessageJobSetup>();
    }


    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));

        services.AddTransient<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpclient) =>
        {
            var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

            httpclient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
        }).AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpclient) =>
        {
            var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

            httpclient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();
    }



    private static void AddAuthorization(IServiceCollection services)
    {
        AddRoleMapping(services);

        services.AddScoped<AuthorizationService>();

        services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }



    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Cache") ??
                                throw new ArgumentNullException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }

    private static void AddHealthChecks(IServiceCollection services, IConfiguration configuration)
    {


        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("LearnWellDatabase"))
            .AddRedis(configuration.GetConnectionString("Cache"))
            .AddUrlGroup(new Uri(configuration["KeyCloak:BaseUrl"]), HttpMethod.Get, "keycloak");
    }


    private static void AddApiVersioning(IServiceCollection services)
    {
        services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    private static void AddRoleMapping(IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.CanCreateCourse, p => p.RequireRole(Roles.CourseCreate));
            options.AddPolicy(Policies.CanReadCourse, p => p.RequireRole(Roles.CourseRead));
            options.AddPolicy(Policies.CanUpdateCourse, p => p.RequireRole(Roles.CourseUpdate));
            options.AddPolicy(Policies.CanDeleteCourse, p => p.RequireRole(Roles.CourseDelete));

            options.AddPolicy(Policies.CanCreateClass, p => p.RequireRole(Roles.ClassCreate));
            options.AddPolicy(Policies.CanReadClass, p => p.RequireRole(Roles.ClassRead));
            options.AddPolicy(Policies.CanUpdateClass, p => p.RequireRole(Roles.ClassUpdate));
            options.AddPolicy(Policies.CanDeleteClass, p => p.RequireRole(Roles.ClassDelete));

            options.AddPolicy(Policies.CanCreateStudent, p => p.RequireRole(Roles.StudentCreate));
            options.AddPolicy(Policies.CanReadStudent, p => p.RequireRole(Roles.StudentRead));
            options.AddPolicy(Policies.CanUpdateStudent, p => p.RequireRole(Roles.StudentUpdate));
            options.AddPolicy(Policies.CanDeleteStudent, p => p.RequireRole(Roles.StudentDelete));

            options.AddPolicy(Policies.CanManageEnrollment, p => p.RequireRole(Roles.EnrollmentManage));
            options.AddPolicy(Policies.CanViewEnrollment, p => p.RequireRole(Roles.EnrollmentView));

            options.AddPolicy(Policies.CanViewMyCourses, p => p.RequireRole(Roles.MyCoursesRead));
            options.AddPolicy(Policies.CanViewMyClasses, p => p.RequireRole(Roles.MyClassesRead));
            options.AddPolicy(Policies.CanViewClassmates, p => p.RequireRole(Roles.ClassmatesRead));
        });
    }
}
