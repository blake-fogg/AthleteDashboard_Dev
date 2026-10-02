using AthleteDashboard.Api.Data;
using AthleteDashboard.Api.Services;
using AthleteDashboard.Api.Repositories;
using AthleteDashboard.Api.Models.Strava;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<SqlConnectionFactory>();
builder.Services.AddScoped<AthleteRepository>();
builder.Services.AddScoped<AthleteService>();
builder.Services.AddHttpClient<StravaService>();
builder.Services.Configure<StravaOptions>(builder.Configuration.GetSection("Strava"));
builder.Services.AddHttpClient<StravaService>();
builder.Services.Configure<StravaOptions>(builder.Configuration.GetSection("Strava"));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        // For local development, allow the frontend no matter which dev port it is running on.
        // In production, replace this with your specific frontend origin(s).
        policy.SetIsOriginAllowed(origin => true)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

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
