using RatingService.Application.Services;
using RatingService.Application.Services.Abstractions;
using RatingService.Common.Models.Settings;
using RatingService.Domain.Abstraction;
using RatingService.Domain.Abstractions;
using RatingService.Infrastructure;
using RatingService.Infrastructure.Abstractions;
using RatingService.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
builder.Services.Configure<ConnectionStringsSettings>(builder.Configuration.GetSection("ConnectionStrings"));

// Configure Db depending on the environment
builder.AddDbConfiguration(builder.Configuration);
builder.AddConfigurations();
builder.AddRepositories();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(); // SCALAR
    app.MapOpenApi();

    //app.UseSwagger();
    //app.UseSwaggerUI();
}

await app.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();