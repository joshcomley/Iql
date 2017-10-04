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
                //var entityDefinition = dataContext.EntityConfigurationContext.GetEntityByType(localEntity.GetType());
                foreach (var property in localEntity.GetType().GetRuntimeProperties())
                {
                    MergeProperty(dataContext, localEntity, remoteEntity, property);
                }
                foreach (var property in remoteEntity.GetType().GetRuntimeProperties())
                {
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
                else if(localValue is IEnumerable)
                {
                    var localNotMatched = new List<object>();
                    var remoteMatched = new List<object>();
                    var localEnumerable = (IEnumerable) localValue;
                    var remoteEnumerable = (IEnumerable) remoteValue;
                    foreach (var local in localEnumerable)
                    {
                        var entityConfiguration = dataContext.EntityConfigurationContext.GetEntityByType(local.GetType());
                        var key = entityConfiguration.Key;
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
                    foreach (var remote in (IEnumerable) remoteValue)
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