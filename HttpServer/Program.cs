using Business.Interfaces;
using Business.Models;
using Business.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IService<Tour>, TourService>();
builder.Services.AddTransient<IService<TourLog>, TourLogService>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
