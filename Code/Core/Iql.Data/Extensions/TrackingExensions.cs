using System;
using System.Collections.Generic;
using Iql.Conversion;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Queued;
using Iql.Data.Tracking;
using Iql.Entities;
using Iql.Extensions;
using Newtonsoft.Json;

namespace Iql.Data.Extensions
{
    public static class TrackingExensions
    {
        public static Guid? EnsureGuid(this object persistenceKey)
        {
            if (persistenceKey is string)
            {
                return new Guid(persistenceKey as string);
            }
            if (persistenceKey is Guid)
            {
                return (Guid)persistenceKey;
            }
            return null;
        }

        public static IQueuedOperation[] GetQueuedChanges(
            this IDataChangeProvider changeProvider, 
            IDataContext dataContext,
            object[] entities = null, 
            IProperty[] properties = null)
        {
            var changes = new List<IQueuedOperation>();
            changeProvider.GetDeletions(dataContext, entities).ForEach(deletion =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedDeleteEntityOperation<>).MakeGenericType(deletion.EntityType), deletion, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            changeProvider.GetUpdates(dataContext, entities, properties).ForEach(update =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedUpdateEntityOperation<>).MakeGenericType(update.EntityType), update, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            changeProvider.GetInserts(dataContext, entities).ForEach(insert =>
            {
                var queuedOperation =
                    Activator.CreateInstance(
                        typeof(QueuedAddEntityOperation<>).MakeGenericType(insert.EntityType), insert, null);
                changes.Add((IQueuedOperation)queuedOperation);
            });
            return changes.ToArray();
        }
    }
}