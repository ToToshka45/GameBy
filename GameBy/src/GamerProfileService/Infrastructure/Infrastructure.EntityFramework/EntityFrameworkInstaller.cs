using GameBy.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EntityFramework
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection ConfigureContext( this IServiceCollection services,
            string connectionString )
        {
            services.AddDbContext<ApplicationDBContext>( optionsBuilder
                => optionsBuilder
                    //.UseLazyLoadingProxies() // lazy loading
                    .UseNpgsql( connectionString ) );
            //.UseSqlite( connectionString ) );
            //.UseSqlServer(connectionString));
            //optionsBuilder.UseSqlite( builder.Configuration[ "SQLite_ConnectionString" ] );

            #region health checks

            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDBContext>(
                    tags: new[] { "db_ef_healthcheck" },
                    customTestQuery: async ( context, token ) =>
                    {
                        return await context.Gamers.AnyAsync( token );
                    } );

            #endregion

            return services;
        }
    }
}
