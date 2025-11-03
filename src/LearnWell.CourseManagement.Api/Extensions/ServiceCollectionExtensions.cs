namespace LearnWell.CourseManagement.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenApiWithJwtAuth(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Components ??= new();
                document.Components.SecuritySchemes = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiSecurityScheme>
                {
                    ["Bearer"] = new()
                    {
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "Enter your JWT token below"
                    }
                };

                document.SecurityRequirements = new List<Microsoft.OpenApi.Models.OpenApiSecurityRequirement>
        {
            new()
            {
                [ new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    { Reference = new Microsoft.OpenApi.Models.OpenApiReference
                      { Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, Id = "Bearer" } }
                ] = new string[] { }
            }
        };

                return Task.CompletedTask;
            });
        });
        return services;

    }

}
