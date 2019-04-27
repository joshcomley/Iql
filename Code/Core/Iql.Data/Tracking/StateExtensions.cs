using System.Linq;
using Iql.Conversion.State;
using Iql.Entities;

namespace Iql.Data.Tracking
{
    public static class StateExtensions
    {
        public static CompositeKey ToCompositeKey(this SerializedCompositeKey key, IEntityConfiguration entityConfiguration)
        {
            var compositeKey = new CompositeKey(entityConfiguration.TypeName, key.Keys.Length);
            for (var i = 0; i < key.Keys.Length; i++)
            {
                var part = key.Keys[i];
                compositeKey.Keys[i] = new KeyValue(part.Name, part.Value,
                    entityConfiguration.Properties.Single(_ => _.PropertyName == part.Name).TypeDefinition);
                compositeKey.Keys[i].Name = part.Name;
                compositeKey.Keys[i].Value = part.Value;
            }
            return compositeKey;
        }
    }
}