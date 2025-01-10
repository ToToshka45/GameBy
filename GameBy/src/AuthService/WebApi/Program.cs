using Application;
using Application.EventHandlers;
using DataAccess;
using DataAccess.Abstractions;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;
using WebApi.MapperProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//amqps://
//var PgConnect = Environment.GetEnvironmentVariable("PG_CONNECT");
//var RedisConnect= Environment.GetEnvironmentVariable("REDIS_CONNECT");
//var RabbitConnect= Environment.GetEnvironmentVariable("RABBIT_CONNECT");

var PgConnect = "Host=localhost;Port=5433;Database=usersdb;Username=postgres;Password=123w";
var RedisConnect= "localhost:1920";


builder.Services.AddDbContext<DataContext>(x =>
{
    //x.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=123w");
    x.UseNpgsql(PgConnect);
    x.UseLazyLoadingProxies();
    x.LogTo(Console.WriteLine, LogLevel.Information);
});


builder.Services.AddSingleton<RabbitService>();
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<AuthenticantionService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));



builder.Services.AddAutoMapper(typeof(AppMappingProfiles));

builder.Services.AddScoped<IDbInitializer, TempDataFactory>();

//"localhost:1919"
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(RedisConnect));

builder.Services.AddScoped<UserTokenService>();

builder.Services.AddTransient<UserAddedEventHandler>();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AppDomain.CurrentDomain.Load("Application")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();
/* for Cookie variant
app.UseCors(builder => builder
    .AllowCredentials()
    .WithOrigins("http://localhost:5173", "https://localhost:5173")
    .AllowAnyMethod()
    .AllowAnyHeader());*/

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Initialize the database (if needed)
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.InitializeDb();

    RabbitService rabbitService = scope.ServiceProvider.GetRequiredService<RabbitService>();
    await rabbitService.Init(""); 
}

app.Run();



