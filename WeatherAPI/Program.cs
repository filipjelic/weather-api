using Weather.Application;
using Weather.Application.Options;
using WeatherAPI.Extensions;
using WeatherAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Controllers
builder.Services.AddControllers();

// Add Http Client(s)
builder.Services.RegisterHttpClient();

// Add Rate limiter
builder.Services.RegisterRateLimiter();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

// Bind options
builder.Services.AddOptions<WeatherOptions>()
    .Bind(builder.Configuration.GetSection(WeatherOptions.Key))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddMemoryCache();

builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// TODO Add healthcheck - check if WeatherAPI supports some kind
//app.MapHealthChecks("health");

// Global exception middleware
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.UseRateLimiter();

app.Run();