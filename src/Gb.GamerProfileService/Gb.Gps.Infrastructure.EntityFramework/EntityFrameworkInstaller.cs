using GameBy.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EntityFramework
{
    public static class EntityFrameworkInstaller
    {
        public static IServiceCollection InstallContext( this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<ApplicationDBContext>( options =>
            {
                options.UseNpgsql( configuration.GetConnectionString( "GameByGamerProfileServiceDb" ) );
                //.UseSqlite( connectionString ) );
                options.UseLazyLoadingProxies(); // lazy loading
            } );

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
