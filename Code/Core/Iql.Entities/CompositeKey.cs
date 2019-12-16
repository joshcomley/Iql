using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Iql.Conversion;
using Iql.Entities.Extensions;
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Entities
{
    [DebuggerDisplay("{FullKeyString}")]
    public class CompositeKey : IJsonSerializable
    {
        public static bool IsValid(IEntityConfiguration entityConfiguration, object entity, bool isNew)
        {
            if ((entityConfiguration.Key.Properties.All(p => !p.CanWrite) || !entityConfiguration.Key.CanWrite) &&
                !isNew)
            {
                return true;
            }

            for (var i = 0; i < entityConfiguration.Key.Properties.Length; i++)
            {
                var property = entityConfiguration.Key.Properties[i];
                if (!property.CanWrite)
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

        public void ApplyTo(object entity)
        {
            foreach (var property in Keys)
            {
                entity.SetPropertyValueByName(property.Name, property.Value);
            }
        }

        public static CompositeKey FromKeyString(string keyString, IEntityConfigurationBuilder builder, IEntityConfiguration entityConfiguration = null)
        {
            var indexOfTypeNameEnd = keyString.IndexOf(">");
            string typeName = "";
            if (indexOfTypeNameEnd != -1)
            {
                typeName = keyString.Substring(0, indexOfTypeNameEnd);
                keyString = keyString.Substring(typeName.Length + 1);
            }

            entityConfiguration = entityConfiguration ?? builder.GetEntityByTypeName(typeName);
            var parts = keyString.Split(';');
            var compositeKey = new CompositeKey(entityConfiguration.TypeName, parts.Length);
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

        public static CompositeKey Ensure(object entityOrKey, IEntityConfiguration entityConfiguration = null)
        {
            return EnsureWithBuilder(entityOrKey, entityConfiguration?.Builder, entityConfiguration);
        }

        public static CompositeKey EnsureWithBuilder(object entityOrKey, IEntityConfigurationBuilder entityConfigurationBuilder,
            IEntityConfiguration entityConfiguration = null)
        {
            if (entityConfigurationBuilder == null && entityConfiguration != null)
            {
                entityConfigurationBuilder = entityConfiguration.Builder;
            }
            if (entityOrKey is string)
            {
                return FromKeyString((string)entityOrKey, entityConfigurationBuilder, entityConfiguration);
            }
            if (entityOrKey == null)
            {
                return null;
            }

            if (entityConfiguration == null)
            {
                entityConfiguration = entityConfigurationBuilder.GetEntityByType(entityOrKey.GetType());
                if (entityConfiguration != null)
                {
                    return ExtractCompositeKey(entityOrKey, entityConfiguration);
                    //return entityConfiguration.GetCompositeKey(entityOrKey);
                }
            }
            else if (entityOrKey.GetType() == entityConfiguration.Type)
            {
                return ExtractCompositeKey(entityOrKey, entityConfiguration);
            }
            if (entityOrKey is CompositeKey)
            {
                return entityOrKey as CompositeKey;
            }
            if (entityConfiguration != null && entityOrKey.IsValueType())
            {
                return GetCompositeKeyFromSingularKey(entityOrKey, entityConfiguration);
            }
            return ExtractCompositeKey(entityOrKey, entityConfiguration);
        }

        private static CompositeKey ExtractCompositeKey(object entityOrKey, IEntityConfiguration entityConfiguration)
        {
            CompositeKey compositeKey;
            compositeKey = new CompositeKey(entityConfiguration.TypeName, entityConfiguration.Key.Properties.Length);
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

            var compositeKey = new CompositeKey(entityConfiguration.TypeName, 1);
            var propertyName = entityConfiguration.Key.Properties.First().Name;
            compositeKey.Keys[0] = new KeyValue(
                propertyName,
                key,
                entityConfiguration.FindProperty(propertyName).TypeDefinition
            );
            return compositeKey;
        }

        public string FullKeyString => this.AsKeyString(true);

        public string TypeName { get; set; }
        private static bool _allInitialized;
        private static List<CompositeKey> _all;

        public static List<CompositeKey> All { get { if (!_allInitialized) { _allInitialized = true; _all = new List<CompositeKey>(); } return _all; } set { _allInitialized = true; _all = value; } }

        public CompositeKey(string typeName, int size)
        {
            //All.Add(this);
            Keys = new KeyValue[size];
            TypeName = typeName;
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