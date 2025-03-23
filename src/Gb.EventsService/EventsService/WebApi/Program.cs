

using Application;
using Application.MapperProfiles;
using DataAccess;
using DataAccess.Abstractions;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using WebApi.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var pgConnect = Environment.GetEnvironmentVariable("PG_CONNECT");
var pgConnect = "Host=localhost;Port=5433;Database=eventsdb;Username=postgres;Password=123w";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x =>
{
    //x.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=123w");
    x.UseNpgsql(pgConnect);
    x.UseLazyLoadingProxies();
    x.LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins(builder.Configuration["CORS:Origins"] ?? "http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            ;
    });
});

builder.Services.AddSingleton<RabbitMqService>();
builder.Services.AddSingleton<MinioService>();
builder.Services.AddScoped<IDbInitializer, TempDataFactory>();
builder.Services.AddAutoMapper(typeof(WebApiMappingProfiles), typeof(ApplicationMappingProfiles));

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IRepository<Event>, EventsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

// Initialize the database (if needed)
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.InitializeDb();

    RabbitMqService rabbitService = scope.ServiceProvider.GetRequiredService<RabbitMqService>();
    await rabbitService.Init("");
}

app.Run();
