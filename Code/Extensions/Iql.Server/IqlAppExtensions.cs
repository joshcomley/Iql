using System;
using Iql.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
// ReSharper disable UnusedMember.Global

namespace Iql.Server
{
    public static class IqlAppExtensions
    {
        public static IServiceCollection AddIql(this IServiceCollection services)
        {
            services.AddSingleton<IAssemblyProvider, DefaultAssemblyProvider>();
            services.AddSingleton<IEntityConfigurationProvider, EntityConfigurationProvider>();
            return services;
        }

        public static IEntityConfigurationBuilder UseIql<TService>(this IApplicationBuilder app, Action<IqlConfiguration> configure = null)
        {
            var serviceProvider = app.ApplicationServices;
            var config = new IqlConfiguration(app);
            configure?.Invoke(config);
            config.Key = config.Key ?? typeof(TService).FullName;
            var provider = serviceProvider.GetService<IEntityConfigurationProvider>() as EntityConfigurationProvider;
            var builder = new EntityConfigurationBuilder();
            provider.Set(config.Key, builder);
            var assemblyProvider = serviceProvider.GetService<IAssemblyProvider>();
            var iqlConfigurator = new IqlConfigurator<TService>();
            iqlConfigurator.Configure(assemblyProvider, serviceProvider, builder, config);
            return builder;
        }
    }
}
