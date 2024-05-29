using Business.Interfaces;
using Business.Models;
using Business.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<IService<Tour>, TourService>();
builder.Services.AddTransient<IService<TourLog>, TourLogService>();
// TODO: please change. for my sanity.
builder.Services.AddTransient<DataAccess.Data.TourPlannerContext>();
builder.Services.AddTransient<DataAccess.Interfaces.IRepository<DataAccess.Models.Tour>, DataAccess.Repositories.TourRepository>();
builder.Services.AddTransient<DataAccess.Interfaces.IRepository<DataAccess.Models.TourLog>, DataAccess.Repositories.TourLogRepository>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();
