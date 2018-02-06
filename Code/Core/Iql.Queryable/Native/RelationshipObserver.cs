using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Events;

namespace Iql.Queryable.Native
{
    public class RelationshipObserver
    {
        private readonly Dictionary<object, bool> _observed = new Dictionary<object, bool>();

        private readonly Dictionary<Type, Dictionary<string, IProperty>> _properties
            = new Dictionary<Type, Dictionary<string, IProperty>>();

        static RelationshipObserver()
        {
            ObserveAllTypedMethod = typeof(RelationshipObserver)
                .GetMethod(nameof(ObserveAllTyped));
        }

        public RelationshipObserver(IDataContext dataContext)
        {
            DataContext = dataContext;
            EntityConfigurationContext = dataContext.EntityConfigurationContext;
        }

        public IDataContext DataContext { get; }

        public EntityConfigurationBuilder EntityConfigurationContext { get; set; }

        public static MethodInfo ObserveAllTypedMethod { get; set; }

        public void ObserveList(IList list, Type entityType)
        {
            ObserveAllTypedMethod.InvokeGeneric(
                this,
                new object[] {list},
                entityType);
        }

        public void ObserveAll(Dictionary<Type, IList> dictionary)
        {
            foreach (var item in dictionary)
            {
                ObserveAllTypedMethod.InvokeGeneric(
                    this,
                    new object[] {item.Value},
                    item.Key);
            }
        }

        public void ObserveAllTyped<T>(List<T> list)
            where T : class
        {
            var entityConfiguration = EntityConfigurationContext.GetEntity<T>();
            Dictionary<string, IProperty> lookup;
            if (!_properties.ContainsKey(typeof(T)))
            {
                lookup = new Dictionary<string, IProperty>();
                _properties.Add(typeof(T), lookup);
                var matches = entityConfiguration.AllRelationships();
                for (var i = 0; i < matches.Count; i++)
                {
                    var relationship = matches[i];
                    lookup.Add(relationship.ThisEnd.Property.Name, relationship.ThisEnd.Property);
                    var properties = relationship.ThisEnd.Constraints();
                    for (var j = 0; j < properties.Length; j++)
                    {
                        var property = properties[j];
                        if (!lookup.ContainsKey(property.Name))
                        {
                            lookup.Add(property.Name, property);
                        }
                    }
                }
            }
            else
            {
                lookup = _properties[typeof(T)];
            }

            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (!_observed.ContainsKey(item))
                {
                    var entity = item as IEntity;
                    if (entity != null)
                    {
                        _observed.Add(item, true);
                        if (entity.PropertyChanged == null)
                        {
                            entity.PropertyChanged = new EventEmitter<IPropertyChangeEvent>();
                        }
                        entity.PropertyChanged.Subscribe(e => { PropertyChangeEvent(e, entityConfiguration, lookup); });
                    }
                }
            }
        }

        private void PropertyChangeEvent(
            IPropertyChangeEvent propertyChangeEvent,
            IEntityConfiguration entityConfiguration,
            Dictionary<string, IProperty> lookup)
        {
            if (lookup.ContainsKey(propertyChangeEvent.PropertyName))
            {
                var property = lookup[propertyChangeEvent.PropertyName];
                switch (property.Kind)
                {
                    case PropertyKind.RelationshipKey:
                        int a = 0;
                        break;
                    case PropertyKind.Relationship:
                        break;
                }
            }
        }
    }
}