using Iql.Entities;

namespace Iql.Server
{
    public interface IIqlEntitySetConfigurator
    {
        void Configure(IEntityConfigurationBuilder builder);
    }
}