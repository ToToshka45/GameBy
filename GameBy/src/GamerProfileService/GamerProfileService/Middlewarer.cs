using AutoMapper;
using Education.Middlewares;
using GamerProfileService.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;

namespace GamerProfileService
{
    public static class Middlewarer
    {
        public static void MapTestMiddlewares( this IApplicationBuilder app )
        {
            app.Map( "/test_1", appBuilder =>
            {
                //appBuilder.UseMiddleware<>();
            } );

            // Можно проверить различные свойства у запроса
            app.MapWhen( context => context.Request.Path.StartsWithSegments( "/test_1" ), appBuilder =>
            {
                //appBuilder.UseMiddleware<>();
            } );
        }

        public static void UseTestMiddlewares( this IApplicationBuilder app )
        {
            //Логирование запроса
            //app.UseSimpleHttpLogging();
            //app.UseHttpLogging();

            //Обработка исключений
            //app.UseSimpleExceptionHandling();
            //app.UseExceptionHandler(options => { });

            //Антитроттлинг
            //app.UseSimpleRateLimiter();
            //app.UseRateLimiter(); //https://blog.maartenballiauw.be/post/2022/09/26/aspnet-core-rate-limiting-middleware.html // Работает

            //Хелсчек
            app.UseHealthChecks( "/health" );
            app.UseHealthChecks( "/samplehealth", new HealthCheckOptions()
            {
                Predicate = healthCheck => healthCheck.Tags.Contains( "SampleHealthCheck" )
            } );

            //Метрики для прометеуса
            app.UseMetricServer();
            //app.UseMiddleware<ResponseMetricMiddleware>();
        }
    }
}
