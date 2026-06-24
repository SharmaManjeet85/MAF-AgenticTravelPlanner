using FluentValidation;
using MAFTravelPlanner.Api.DependencyInjection;
using MAFTravelPlanner.Application.Validation;
using MAFTravelPlanner.Api.Endpoints;
using MAFTravelPlanner.Api.Middleware;
using MAFTravelPlanner.Infrastructure.DependencyInjection;
// using Microsoft.Extensions.Options;
// using MAFTravelPlanner.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddTravelPlannerServices();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<TravelRequestValidator>();
var app = builder.Build();

// var scope = app.Services.CreateScope();

// var options = scope.ServiceProvider
//     .GetRequiredService<IOptions<OllamaOptions>>();

// Console.WriteLine(
//     $"Model: {options.Value.Model}");
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};
app.MapAiEndpoints();
app.MapTravelEndpoints();
app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
