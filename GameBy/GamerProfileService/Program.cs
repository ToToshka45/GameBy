
using GameBy.Core.Abstractions.Repositories;
using GameBy.DataAccess;
using GameBy.DataAccess.Repositories;
using GamerProfileService.Settings;
using Microsoft.EntityFrameworkCore;

namespace GamerProfileService;

public class Program
{
    public static void Main( string[] args )
    {
        var builder = WebApplication.CreateBuilder( args );

        // Add services to the container.

        builder.Services.AddControllers();

        var applicationSettings = builder.Configuration.Get<ApplicationSettings>();
        builder.Services.AddDbContext<ApplicationDBContext>( optionsBuilder => {
            optionsBuilder.UseNpgsql( applicationSettings.PostgreSQL_ConnectionString );
            //optionsBuilder.UseSqlite( builder.Configuration[ "SQLite_ConnectionString" ] );
        } );

        builder.Services.AddTransient<IGamerRepository, GamerRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using ( var scope = app.Services.CreateScope() )
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            db.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if ( app.Environment.IsDevelopment() )
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

}
