using RatingService.Common.Models.Settings;
using RatingService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<ConnectionStringsSettings>("appsettings.json");

// Configure Db depending on the environment
builder.AddDbConfiguration(builder.Configuration, builder.Environment.IsDevelopment());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // TODO: add OpenAPI docs
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
