using System;
using System.Collections.Generic;
using Iql.Data.Configuration;
using Iql.Queryable.Extensions;

namespace Iql.Data.Relationships
{
    public class PropertyChangeIgnorer
    {
        private readonly Dictionary<IProperty, Dictionary<object, object>> _changingProperties =
            new Dictionary<IProperty, Dictionary<object, object>>();

        public bool AreAnyIgnored(IProperty[] properties, params object[] entities)
        {
            var isIgnored = false;
            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                if (_changingProperties.ContainsKey(property))
                {
                    var changingProperty = _changingProperties[property];
                    foreach (var entity in entities)
                    {
                        if (entity != null && changingProperty.ContainsKey(entity))
                        {
                            isIgnored = true;
                            break;
                        }
                    }
                }
            }

            return isIgnored;
        }

        public void IgnoreAndRunIfNotAlreadyIgnored(Action action,
            IProperty[] properties,
            params object[] entities)
        {
            if (!AreAnyIgnored(properties, entities))
            {
                IgnoreChanges(action, properties, entities);
            }
        }

        public void RunIfNotAlreadyIgnored(Action action,
            IProperty[] properties,
            params object[] entities)
        {
            if (!AreAnyIgnored(properties, entities))
            {
                action();
            }
        }

        public void IgnoreAndRunEvenIfAlreadyIgnored(Action action, IProperty[] properties, params object[] entities)
        {
            IgnoreChanges(action, properties, entities);
        }

        public void IgnoreChanges(Action action, IProperty[] properties, params object[] entities)
        {
            var ignored = new List<KeyValuePair<IProperty, object>>();
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                var propertyLookup = _changingProperties.Ensure(property, () => new Dictionary<object, object>());
                foreach (var entity in entities)
                {
                    if (entity != null && !AreAnyIgnored(new []{ property }, entity))
                    {
                        ignored.Add(new KeyValuePair<IProperty, object>(property, entity));
                        propertyLookup.Add(entity, entity);
                    }
                }
            }

            action();

            foreach (var pair in ignored)
            {
                _changingProperties[pair.Key].Remove(pair.Value);
            }
        }
    }
    public class ChangeIgnorer
    {
        private readonly Dictionary<object, object> _changingEntities =
            new Dictionary<object, object>();

        public bool AreAnyIgnored(params object[] entities)
        {
            var isIgnored = false;
            foreach (var entity in entities)
            {
                if (entity != null && _changingEntities.ContainsKey(entity))
                {
                    isIgnored = true;
                    break;
                }
            }

            return isIgnored;
        }

        public void IgnoreAndRunIfNotAlreadyIgnored(Action action, params object[] entities)
        {
            if (!AreAnyIgnored(entities))
            {
                IgnoreChanges(action, entities);
            }
        }

        public void IgnoreAndRunEvenIfAlreadyIgnored(Action action, params object[] entities)
        {
            IgnoreChanges(action, entities);
        }

        public void IgnoreChanges(Action action, params object[] entities)
        {
            var ignored = new List<object>();
            foreach (var entity in entities)
            {
                if (entity != null && !AreAnyIgnored(entity))
                {
                    ignored.Add(entity);
                    _changingEntities.Add(entity, entity);
                }
            }

            action();
            foreach (var entity in ignored)
            {
                _changingEntities.Remove(entity);
            }
        }
    }
}