using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class CompositeKey
    {
        public object TryGetValue(string name)
        {
            var pair = Keys.SingleOrDefault(k => k.Name == name);
            if (pair != null)
            {
                return pair.Value;
            }

            return null;
        }

        public object GetValue(string name)
        {
            return Keys.Single(k => k.Name == name).Value;
        }

        public static CompositeKey Ensure(object key, IEntityConfiguration entityConfiguration)
        {
            CompositeKey compositeKey;
            if (key is CompositeKey)
            {
                compositeKey = key as CompositeKey;
            }
            else
            {
                compositeKey = GetCompositeKeyFromSingularKey(key, entityConfiguration);
            }

            return compositeKey;
        }

        public static CompositeKey GetCompositeKeyFromSingularKey(object key, IEntityConfiguration entityConfiguration)
        {
            if (key is CompositeKey)
            {
                return key as CompositeKey;
            }

            var compositeKey = new CompositeKey(1);
            var propertyName = entityConfiguration.Key.Properties.First().Name;
            compositeKey.Keys[0] = new KeyValue(
                propertyName,
                key,
                entityConfiguration.FindProperty(propertyName).TypeDefinition
            );
            return compositeKey;
        }

        public static List<CompositeKey> All { get; set; }
        = new List<CompositeKey>();
        public CompositeKey(int size)
        {
            //All.Add(this);
            Keys = new KeyValue[size];
        }
        public object Entity { get; set; }
        public KeyValue[] Keys { get; }

        public bool HasDefaultValue()
        {
            // Avoid for loop in most casesS
            if (Keys.Length == 1)
            {
                return Keys[0].HasDefaultValue;
            }

            if (Keys.Length == 2)
            {
                return Keys[0].HasDefaultValue ||
                       Keys[1].HasDefaultValue;
            }
            for (var i = 0; i < Keys.Length; i++)
            {
                if (Keys[i].HasDefaultValue)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Matches(CompositeKey compositeKey)
        {
            foreach (var key in compositeKey.Keys)
            {
                var matchedKey = Keys.SingleOrDefault(k => k.Name == key.Name);
                if (matchedKey == null)
                {
                    return false;
                }
                if (!Equals(matchedKey.Value, key.Value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}