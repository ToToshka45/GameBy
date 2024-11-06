using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RatingService.Common.Models.Settings;
using RatingService.Infrastructure.DataAccess;

namespace RatingService.Infrastructure;

public static class DiExtension
{
    public static void AddDbConfiguration(this IHostApplicationBuilder builder, IConfiguration config, bool isDev)
    {
        var connStrings = config.GetSection("ConnectionStrings").Get<ConnectionStringsSettings>();
        if (connStrings is null) { throw new Exception("Could not get the ConnectionsString section from appsettings.json."); }

        if (isDev)
        {
            builder.Services.AddDbContextFactory<RatingServiceDbContext>(options =>
            {
                options.UseSqlite(connStrings.SQLiteConnectionString);
                options.LogTo(Console.WriteLine, LogLevel.Debug);
            });
        }
        else
        {
            builder.Services.AddDbContextFactory<RatingServiceDbContext>(options =>
            {
                options.UseSqlite(connStrings.NpgsqlConnectionString);
                options.LogTo(Console.WriteLine, LogLevel.Trace);
            });
        }
    
    }
}
