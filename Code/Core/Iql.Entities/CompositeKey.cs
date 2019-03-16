using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.Conversion;
using Iql.Entities.Extensions;
using Newtonsoft.Json;

namespace Iql.Entities
{
    [DebuggerDisplay("{KeyString}")]
    public class CompositeKey : IJsonSerializable
    {
        public static bool IsValid(IEntityConfiguration entityConfiguration, object entity, bool isNew)
        {
            if ((entityConfiguration.Key.Properties.All(p => p.IsReadOnly) || !entityConfiguration.Key.Editable) &&
                !isNew)
            {
                return true;
            }

            for (var i = 0; i < entityConfiguration.Key.Properties.Length; i++)
            {
                var property = entityConfiguration.Key.Properties[i];
                if (property.IsReadOnly)
                {
                    continue;
                }

                if (property.GetValue(entity).IsDefaultValue(
                    property.TypeDefinition))
                {
                    return false;
                }
            }

            return true;
        }
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

        public static CompositeKey Ensure(object entityOrKey, IEntityConfiguration entityConfiguration)
        {
            if (entityOrKey == null)
            {
                return null;
            }
            if (entityOrKey.GetType() == entityConfiguration.Type)
            {
                return ExtractCompositeKey(entityOrKey, entityConfiguration);
                //return entityConfiguration.GetCompositeKey(entityOrKey);
            }
            if (entityOrKey is CompositeKey)
            {
                return entityOrKey as CompositeKey;
            }
            if (entityOrKey.IsValueType())
            {
                return GetCompositeKeyFromSingularKey(entityOrKey, entityConfiguration);
            }
            return ExtractCompositeKey(entityOrKey, entityConfiguration);
        }

        private static CompositeKey ExtractCompositeKey(object entityOrKey, IEntityConfiguration entityConfiguration)
        {
            CompositeKey compositeKey;
            compositeKey = new CompositeKey(entityConfiguration.Key.Properties.Length);
            for (var i = 0; i < entityConfiguration.Key.Properties.Length; i++)
            {
                var property = entityConfiguration.Key.Properties[i];
                compositeKey.Keys[i] =
                    new KeyValue(property.Name, entityOrKey.GetPropertyValue(property), property.TypeDefinition);
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

        private string KeyString => this.AsKeyString();

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
            // Avoid for loop in most cases
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

        public bool Matches(CompositeKey compositeKey, bool treatBothEmptyAsEquivalent = false)
        {
            return AreEquivalent(compositeKey, this);
        }

        public static bool AreEquivalent(CompositeKey left, CompositeKey right, bool treatBothEmptyAsEquivalent = false)
        {
            if (!treatBothEmptyAsEquivalent)
            {
                if (left.HasDefaultValue() && right.HasDefaultValue())
                {
                    return false;
                }
            }
            foreach (var key in left.Keys)
            {
                var matchedKey = right.Keys.SingleOrDefault(k => k.Name == key.Name);
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

        public string SerializeToJson()
        {
            return this.ToJson();
        }

        public object PrepareForJson()
        {
            return new
            {
                Keys = Keys?.Select(_ => _.PrepareForJson())
            };
        }
    }
}