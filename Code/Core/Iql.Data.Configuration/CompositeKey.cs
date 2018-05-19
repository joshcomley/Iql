using System.Collections.Generic;
using System.Linq;

namespace Iql.Data.Configuration
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

        public static CompositeKey FromKeyString(string keyString, IEntityConfiguration entityConfiguration)
        {
            var parts = keyString.Split(';');
            var compositeKey = new CompositeKey(parts.Length);
            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                IProperty property;
                string valueStr;
                if (part.Contains(":"))
                {
                    var keyValue = part.Split(':');
                    property = entityConfiguration.FindProperty(keyValue[0]);
                    valueStr = keyValue[1] == "NULL" ? null : keyValue[1];
                    compositeKey.Keys[i] = new KeyValue(keyValue[0], property.TypeDefinition.EnsureValueType(valueStr), property.TypeDefinition);
                }
                else
                {
                    property = entityConfiguration.Key.Properties[i];
                    valueStr = part == "NULL" ? null : part;
                }
                compositeKey.Keys[i] = new KeyValue(property.Name, property.TypeDefinition.EnsureValueType(valueStr), property.TypeDefinition);
            }

            return compositeKey;
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