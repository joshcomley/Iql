using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.DataStores
{
    public class ObjectMerger
    {
        public static void Merge(IDataContext dataContext, object localEntity, object remoteEntity)
        {
            if (remoteEntity != null)
            {
                var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(localEntity.GetType());
                var propertiesMerged = new List<string>();
                foreach (var relationship in entityConfiguration.Relationships)
                {
                    var isSource = relationship.Source.Configuration == entityConfiguration;
                    var sourceRelationship = isSource ? relationship.Source : relationship.Target;
                    var targetRelationship = isSource ? relationship.Target : relationship.Source;
                    var propertyName = sourceRelationship.Property.PropertyName;
                    propertiesMerged.Add(propertyName);
                    var localRelationshipValue = localEntity.GetPropertyValue(propertyName);
                    var remoteRelationshipValue = remoteEntity.GetPropertyValue(propertyName);
                    var nonNull = localRelationshipValue ?? remoteRelationshipValue;
                    if (localRelationshipValue != null && remoteRelationshipValue == null)
                    {
                        localEntity.SetPropertyValue(propertyName, null);
                    }
                    else if (remoteRelationshipValue != null)
                    {
                        // Single entity, no current value locally so just assign
                        if (localRelationshipValue == null)
                        {
                            localEntity.SetPropertyValue(propertyName, remoteRelationshipValue);
                        }
                        else
                        {
                            var isArray = remoteRelationshipValue is IEnumerable && !(remoteRelationshipValue is string);
                            if (isArray)
                            {
                                var localList = (IList)localRelationshipValue;
                                var remoteList = (IList)remoteRelationshipValue;
                                if (localList.Count == 0)
                                {
                                    foreach (var item in remoteList)
                                    {
                                        localList.Add(item);
                                    }
                                }
                                else
                                {
                                    var localListCopy = new List<object>();
                                    foreach (var item in localList)
                                    {
                                        localListCopy.Add(item);
                                    }
                                    localList.Clear();
                                    foreach (var remoteItem in remoteList)
                                    {
                                        object match = null;
                                        foreach (var localItem in localListCopy)
                                        {
                                            var isMatch = true;
                                            foreach (var keyProperty in targetRelationship.Configuration.Properties)
                                            {
                                                if (relationship.Constraints.Any(c => c.SourceKeyProperty.PropertyName == keyProperty.Name))
                                                {
                                                    continue;
                                                }
                                                if (!Equals(remoteItem.GetPropertyValue(keyProperty.Name),
                                                    localItem.GetPropertyValue(keyProperty.Name)))
                                                {
                                                    isMatch = false;
                                                    break;
                                                }
                                            }
                                            if (isMatch)
                                            {
                                                match = localItem;
                                                break;
                                            }
                                        }
                                        if (match == null)
                                        {
                                            // We've found no matching local item in the list, so add it
                                            localList.Add(remoteItem);
                                        }
                                        else
                                        {
                                            Merge(dataContext, match, remoteItem);
                                            localList.Add(match);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Merge(dataContext,
                                    localRelationshipValue, remoteRelationshipValue);
                            }
                        }
                    }
                }
                //var entityDefinition = dataContext.EntityConfigurationContext.GetEntityByType(localEntity.GetType());
                foreach (var property in localEntity.GetType().GetRuntimeProperties())
                {
                    if (propertiesMerged.Contains(property.Name))
                    {
                        continue;
                    }
                    MergeProperty(dataContext, localEntity, remoteEntity, property);
                }
                foreach (var property in remoteEntity.GetType().GetRuntimeProperties())
                {
                    if (propertiesMerged.Contains(property.Name))
                    {
                        continue;
                    }
                    MergeProperty(dataContext, localEntity, remoteEntity, property);
                }
            }
        }

        private static void MergeProperty(IDataContext dataContext, object localEntity, object remoteEntity,
            PropertyInfo property)
        {
            var localValue = property.GetValue(localEntity);
            var remoteValue = property.GetValue(remoteEntity);
            if (localValue != null && remoteValue != null)
            {
                if (localValue.GetType().IsClass && !(localValue is string))
                {
                    Merge(dataContext, localValue, remoteValue);
                }
                else if (localValue is IEnumerable && !(localValue is string))
                {
                    var localNotMatched = new List<object>();
                    var remoteMatched = new List<object>();
                    var localEnumerable = (IEnumerable)localValue;
                    var remoteEnumerable = (IEnumerable)remoteValue;
                    foreach (var local in localEnumerable)
                    {
                        var childEntityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(local.GetType());
                        var key = childEntityConfiguration.Key;
                        var match = true;
                        foreach (var remote in remoteEnumerable)
                        {
                            foreach (var keyProperty in key.Properties)
                            {
                                var localKeyValue = local.GetPropertyValue(keyProperty.PropertyName);
                                var remoteKeyValue = remote.GetPropertyValue(keyProperty.PropertyName);
                                if (!Equals(localKeyValue, remoteKeyValue))
                                {
                                    match = false;
                                    break;
                                }
                            }
                            if (match)
                            {
                                remoteMatched.Add(remote);
                                Merge(dataContext, local, remote);
                                break;
                            }
                        }
                        if (!match)
                        {
                            localNotMatched.Add(local);
                        }
                    }
                    var remoteNotMatched = new List<object>();
                    foreach (var remote in (IEnumerable)remoteValue)
                    {
                        if (!remoteMatched.Contains(remote))
                        {
                            remoteNotMatched.Add(remote);
                        }
                    }
                    foreach (var toAdd in remoteNotMatched)
                    {
                        ((IList)localValue).Add(toAdd);
                    }
                    foreach (var toRemove in localNotMatched)
                    {
                        ((IList)localValue).Remove(toRemove);
                    }
                }
                else
                {
                    property.SetValue(localEntity,
                        property.GetValue(remoteEntity));
                }
            }
            else
            {
                property.SetValue(localEntity,
                    property.GetValue(remoteEntity));
            }
        }
    }
}