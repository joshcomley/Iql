using System;
using Iql.Entities;

namespace Iql.Server
{
    public interface IEntityConfigurationProvider
    {
        IEntityConfigurationBuilder Get(string key);
        IEntityConfigurationBuilder Get(Type serviceType);
        IEntityConfigurationBuilder Get<T>();
    }
}