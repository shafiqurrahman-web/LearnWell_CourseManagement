using HealthChecks.UI.Client;
using LearnWell.CourseManagement.Api.Extensions;
using LearnWell.CourseManagement.Application;
using LearnWell.CourseManagement.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddOpenApi();
builder.Services.AddOpenApiWithJwtAuth();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.MapOpenApi("/openapi/v1/openapi.json");

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1/openapi.json", "LearnWell.CourseManagement.Api v1");        
    });
}


//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
