using AchievementProfileService.Mapping;
using AutoMapper;
using Education.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using GameBy.DataAccess.Repositories;
using GamerProfileService.Mapping;
using Gb.Gps.Services.Abstractions;
using Gb.Gps.Services.Implementations;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RankProfileService.Mapping;
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
            //var applicationSettings = configuration.Get<ApplicationSettings>();

            services.InstallAutomapper()
                    //.AddSingleton( applicationSettings )
                    .AddSingleton( (IConfigurationRoot) configuration )
                    .InstallSystemCache()
                    .InstallSystemLimiter()
                    .InstallHealthChecks()
                    //.InstallLogging()
                    .InstallServices()
                    .InstallContext( configuration )
                    .InstallRepositories()
                    .InstallFluentValidation()
                    .InstallCache( configuration );

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
                cfg.AddProfile<RankMappingsProfile>();
                cfg.AddProfile<AchievementMappingsProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.GamerMappingsProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.RankMappingsProfile>();
                cfg.AddProfile<Services.Implementations.Mapping.AchievementMappingsProfile>();
            } );

            configuration.AssertConfigurationIsValid();

            return configuration;
        }

        private static IServiceCollection InstallServices( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddTransient<IGamerService, GamerService>();
            serviceCollection.AddTransient<IRankService, RankService>();
            serviceCollection.AddTransient<IAchievementService, AchievementService>();

            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddTransient<IGamerRepository, GamerRepository>()
                             .AddTransient<IRankRepository, RankRepository>()
                             .AddTransient<IAchievementRepository, AchievementRepository>()
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

        private static IServiceCollection InstallFluentValidation( this IServiceCollection serviceCollection )
        {
            serviceCollection.AddFluentValidationAutoValidation();
            serviceCollection.AddFluentValidationClientsideAdapters();

            // Если валидаторов много, то можно сразу зарегистрировать валидаторы для конкретной Assembly.
            serviceCollection.AddValidatorsFromAssemblyContaining<Program>();

            return serviceCollection;
        }

        private static IServiceCollection InstallCache( this IServiceCollection serviceCollection, IConfiguration configuration )
        {
            // distributed cache
            serviceCollection.AddDistributedMemoryCache();
            serviceCollection.AddStackExchangeRedisCache( options =>
            {
                options.Configuration = configuration.GetConnectionString( "GameByGamerProfileServiceRedis" );
            } );

            serviceCollection.AddScoped<ICacheService, CacheService>();

            return serviceCollection;
        }
    }
}
