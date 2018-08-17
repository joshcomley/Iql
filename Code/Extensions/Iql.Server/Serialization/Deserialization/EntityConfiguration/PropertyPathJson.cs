using Iql.Entities;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class PropertyPathJson : PropertyPath
    {
        public PropertyPathJson(IEntityConfiguration configuration, string path, string key = null) : base(configuration, path, key)
        {
        }
        public PropertyPath SetEntityConfiguration(IEntityConfiguration configuration)
        {
            _entityConfiguration = configuration;
            if (configuration != null)
            {
                try
                {
                    _property = configuration.FindNestedProperty(Path);
                }
                catch { }
            }

            return this;
        }
    }
}