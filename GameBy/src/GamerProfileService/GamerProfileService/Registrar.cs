using AutoMapper;
using Education.Middlewares;
using GameBy.DataAccess.Repositories;
using GamerProfileService.Mapping;
using GamerProfileService.Settings;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;
using System.Threading.RateLimiting;

namespace GamerProfileService
{
    public static class Registrar
    {
        public static IServiceCollection AddServices( this IServiceCollection services, IConfiguration configuration )
        {
            var applicationSettings = configuration.Get<ApplicationSettings>();

            services.InstallAutomapper()
                    .AddSingleton( applicationSettings )
                    .AddSingleton( (IConfigurationRoot) configuration )
                    .InstallSystemCache()
                    .InstallSystemLimiter()
                    .InstallHealthChecks()
                    //.InstallLogging()
                    .InstallServices()
                    .ConfigureContext( applicationSettings.PostgreSQL_ConnectionString )
                    .InstallRepositories();

            return services;
        }

        private static IServiceCollection InstallAutomapper( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddSingleton<IMapper>( new Mapper( GetMapperConfiguration() ) );

            return serviceCollection;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration( cfg =>
            {
                cfg.AddProfile<GamerMappingsProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.GamerMappingsProfile>();
            } );

            configuration.AssertConfigurationIsValid();

            return configuration;
        }

        private static IServiceCollection InstallServices( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddTransient<IGamerService, GamerService>();

            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddTransient<IGamerRepository, GamerRepository>()
                             .AddTransient<IUnitOfWork, UnitOfWork>();

            return serviceCollection;
        }

        private static IServiceCollection InstallLogging( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddHttpLogging( httpLoggingOptions =>
            {
                httpLoggingOptions.LoggingFields = HttpLoggingFields.All;
            } );

            return serviceCollection;
        }

        private static IServiceCollection InstallSystemCache( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddSingleton<IMemoryCache, MemoryCache>();

            serviceCollection.AddResponseCaching( ( opt ) =>
            {
            } );

            return serviceCollection;
        }

        private static IServiceCollection InstallSystemLimiter( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddRateLimiter( options => {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(
                    httpContext => RateLimitPartition.GetFixedWindowLimiter( partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(), factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes( 1 )
                    } ) );
                options.RejectionStatusCode = 429;
            } );

            return serviceCollection;
        }

        private static IServiceCollection InstallHealthChecks( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddHealthChecks()
                .AddCheck<SampleHealthCheck>(
                "SampleHealthCheck",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[]
                {
                    "SampleHealthCheck"
                } );

            return serviceCollection;
        }
    }
}
