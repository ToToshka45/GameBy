using GameBy.DataAccess;
using GamerProfileService.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

namespace GamerProfileService;

public class Program
{
    public static void Main( string[] args )
    {
        var builder = WebApplication.CreateBuilder( args );

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddServices( builder.Configuration );

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApiDocument( options =>
        {
            options.Title = "Gamer Profile Service API Doc";
            options.Version = "1.0";
        } );

        var app = builder.Build();

        //app.UseHttpLogging();

        using ( var scope = app.Services.CreateScope() )
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            //db.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if ( app.Environment.IsDevelopment() )
        {
            app.UseDeveloperExceptionPage();

            app.UseOpenApi();
            app.UseSwaggerUi( x =>
            {
                x.DocExpansion = "list";
            } );
        }

        app.UseHttpsRedirection();

        app.MapTestMiddlewares();
        app.UseTestMiddlewares();

        // My Middlewares
        // Кеширование запроса
        app.UseLibrarySimpleCaching();
        app.UseResponseCaching(); // TODO: Не работает

        // Хелсчеки
        app.UseHealthChecks( "/db_ef_healthcheck", new HealthCheckOptions // TODO: хз, мб и работает, дёргаю, лога не вижу
        {
            Predicate = healthCheck => healthCheck.Tags.Contains( "db_ef_healthcheck" )
        } );

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
