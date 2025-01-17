using RatingService.Application;
using RatingService.Common.Models.Settings;
using RatingService.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
builder.Services.Configure<ConnectionStringsSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection(nameof(RabbitMQSettings)));
builder.Services.Configure<RabbitMQConfigurations>(builder.Configuration.GetSection(nameof(RabbitMQConfigurations)));

// Configure Db depending on the environment
await builder.MigrateRabbitMQ();
builder.AddDbConfiguration(builder.Configuration);
builder.AddConfigurations();

builder.AddTestingServices();

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