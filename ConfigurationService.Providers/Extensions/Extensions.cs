using Dapper;
using FluentMigrator.Runner;
using ConfigurationService.Database.Entities;
using ConfigurationService.Database.Providers.Interfaces;
using ConfigurationService.Entities.Providers.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace ConfigurationService.Database.Extensions
{
    public static class Extensions
    {

        public static void AddMigratorWithProvider(this IServiceCollection services, DataBaseConfig config, Assembly assembly)
        {

            try {
                string sql = $"CREATE DATABASE   {config.DataBase}";
                using (var connection = new NpgsqlConnection($"Host={config.Host}:{config.Port}; Username={config.Username};Password={config.Password}"))
                {
                    connection.Execute(sql);
                }
            }
            catch (Exception ex)
            {

            }

            services
                    // Add common FluentMigrator services
                    .AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                       // Add Postgres 11 support to FluentMigrator
                       .AddPostgres11_0()
                       .WithGlobalConnectionString($"Host={config.Host}:{config.Port}; Database = {config.DataBase}; Username={config.Username};Password={config.Password}")
                       .ScanIn(assembly).For.All()
                   ); ;


            services.AddScoped<IDbProvider>(p => new DapperProvider(config));

        }
        public static IApplicationBuilder Migrate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp();
            return app;
        }
    }
}
