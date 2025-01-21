using RatingService.Application;
using RatingService.Application.Services;
using RatingService.Common.Models.Settings;
using RatingService.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var isRabbitMqTestRequired = builder.Configuration.GetValue<bool>("TestSettings:IsRabbitMQTestRequired");

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();
builder.Services.Configure<ConnectionStringsSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection(nameof(RabbitMQSettings)));
builder.Services.Configure<RabbitMQConfigurations>(builder.Configuration.GetSection(nameof(RabbitMQConfigurations)));

if (isRabbitMqTestRequired)
    builder.Services.AddScoped<RabbitMQTestSeedService>();

await builder.MigrateRabbitMQ();
builder.AddDbConfiguration(builder.Configuration);
builder.AddConfigurations();

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

if (isRabbitMqTestRequired)
{
    // define what number of Users we want to seed to RabbitMQ
    var usersCount = builder.Configuration.GetValue<int?>("TestSettings:UsersCount") ?? 10;
    await app.SeedRabbitTestMessages(usersCount);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();