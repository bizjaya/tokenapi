using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using TOKENAPI.Mapper;
using TOKENAPI.Models;

namespace TOKENAPI
{
    public static class ServiceExtensions
    {
        public static void AddMappers(this IServiceCollection services)
        {
            services.AddSingleton((ctx )=>
            {

               // Stgs stgs = ctx.GetService<Stgs>();
                var mappingConfig = new MapperConfiguration(mc => {
                    mc.AddProfile(new UsrProfile());
                    mc.AddProfile(new EvtProfile(ctx.GetService<Stgs>()));
                });

                return mappingConfig.CreateMapper();
            });



        }

        public static void AddHangFire(this IServiceCollection services, string constr)
        {
            services.AddHangfire(config =>
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
            .UseStorage(
             new MySqlStorage(constr,
             new MySqlStorageOptions
             {
                 TransactionIsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                 QueuePollInterval = TimeSpan.FromSeconds(15),
                 JobExpirationCheckInterval = TimeSpan.FromHours(1),
                 CountersAggregateInterval = TimeSpan.FromMinutes(5),
                 PrepareSchemaIfNecessary = true,
                 DashboardJobListLimit = 50000,
                 TransactionTimeout = TimeSpan.FromMinutes(1),
                 TablesPrefix = "_hf"
             }))
            );

            services.AddHangfireServer();
        }


        public static void AddCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>
            (options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
        }

    }
}
