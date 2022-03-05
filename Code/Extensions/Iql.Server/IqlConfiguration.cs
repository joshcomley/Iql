using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace Iql.Server
{
    public class IqlConfiguration
    {
        public IServiceProvider Services { get; }
        public string Key { get; set; }
        public List<Action> Bootup { get; set; } = new List<Action>();

        internal IqlConfiguration(IServiceProvider services)
        {
            Services = services;
        }
    }
}