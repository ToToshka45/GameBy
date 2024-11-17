using AutoMapper;
using GameBy.DataAccess.Repositories;
using GamerProfileService.Mapping;
using GamerProfileService.Settings;
using Infrastructure.EntityFramework;
using Services.Abstractions;
using Services.Implementations;
using Services.Repositories.Abstractions;

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
    }
}
