using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Iql.Server
{
    public class IqlConfigurator<T>
    {
        public void Configure(IAssemblyProvider assemblyProvider, IServiceProvider serviceProvider,
            IEntityConfigurationBuilder builder, IqlConfiguration config)
        {
            var configurators = new List<IIqlEntitySetConfigurator>();
            foreach (var assembly in assemblyProvider.CandidateAssemblies)
            {
                foreach (var type in assembly.DefinedTypes)
                {
                    if (typeof(IIqlEntitySetConfigurator).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                    {
                        configurators.Add((IIqlEntitySetConfigurator)ActivatorUtilities.CreateInstance(serviceProvider, type));
                    }
                }
            }

            if (config.Bootup != null && config.Bootup.Any())
            {
                foreach (var bootup in config.Bootup)
                {
                    bootup();
                }
            }

            foreach (var configurator in configurators)
            {
                configurator.Configure(builder);
            }
        }
    }
}