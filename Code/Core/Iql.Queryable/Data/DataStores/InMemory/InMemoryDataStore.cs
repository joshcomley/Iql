using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iql.Queryable.Data.Crud.Operations;
using Iql.Queryable.Data.Crud.Operations.Queued;
using Iql.Queryable.Data.Crud.Operations.Results;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.Tracking;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.DataStores.InMemory
{
    public class InMemoryDataStore : DataStore
    {
        public InMemoryDataStore(IQueryableAdapterBase queryableAdapter)
        {
            QueryableAdapter = queryableAdapter;
        }

        public IQueryableAdapterBase QueryableAdapter { get; }

        public override Task<AddEntityResult<TEntity>> PerformAdd<TEntity>(
            QueuedAddEntityOperation<TEntity> operation)
        {
            var data = operation.Operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>()
                .GetSourceByType(operation.Operation.EntityType);
            var clone = operation.Operation.Entity.CloneAs(DataContext, operation.Operation.EntityType, RelationshipCloneMode.DoNotClone);
            var configuration = operation
                .Operation
                .DataContext
                .EntityConfigurationContext
                .GetEntityByType(operation.Operation.EntityType);
            foreach (var property in configuration.Key.Properties)
            {
                if (property.Kind == PropertyKind.Key)
                {
                    var oldId = clone.GetPropertyValue(property);
                    if (property.ElementType == typeof(int))
                    {
                        clone.SetPropertyValue(property, NextIdInteger(data, property));
                    }
                    else if (property.ElementType == typeof(string))
                    {
                        clone.SetPropertyValue(property, NextIdString(data, property));
                    }
                    if (!oldId.IsDefaultValue())
                    {
                        var newId = clone.GetPropertyValue(property);
                        foreach (var relationship in configuration.Relationships)
                        {
                            switch (relationship.Type)
                            {
                                case RelationshipType.OneToOne:
                                    break;
                                case RelationshipType.OneToMany:
                                    break;
                            }
                        }
                    }
                }
            }
            data.Add(clone);
            operation.Result.Success = true;
            operation.Result.RemoteEntity = clone;
            return Task.FromResult(operation.Result);
        }

        public int NextIdInteger(IList data, IProperty property)
        {
            int max = 0;
            foreach (var existingEntity in data)
            {
                var value = (int)existingEntity.GetPropertyValue(property);
                if(value > max)
                {
                    max = value;
                }
            }
            return ++max;
        }

        public string NextIdString(IList data, IProperty property)
        {
            return Guid.NewGuid().ToString();
        }

        private static IList<TEntity> DataSet<TEntity>(ICrudOperation operation)
        {
            return operation.DataContext.GetConfiguration<InMemoryDataStoreConfiguration>().GetSource<TEntity>();
        }

        private int FindEntityIndexFromOperation<TEntity>(EntityCrudOperation<TEntity> operation) where TEntity : class
        {
            return FindEntityIndex(
                operation.EntityType,
                operation.Entity,
                DataSet<TEntity>(operation)
            );
        }

        public override Task<UpdateEntityResult<TEntity>> PerformUpdate<TEntity>(
            QueuedUpdateEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                DataSet<TEntity>(operation.Operation)[index] = operation.Operation.Entity;
            }
            return Task.FromResult(operation.Result);
        }

        public override Task<DeleteEntityResult<TEntity>> PerformDelete<TEntity>(
            QueuedDeleteEntityOperation<TEntity> operation)
        {
            var index = FindEntityIndexFromOperation(operation.Operation);
            if (index != -1)
            {
                DataSet<TEntity>(operation.Operation).RemoveAt(0);
            }
            return Task.FromResult(operation.Result);
        }

        public override Task<GetDataResult<TEntity>> PerformGet<TEntity>(QueuedGetDataOperation<TEntity> operation)
        {
            var q = operation.Operation.Queryable.ToQueryWithAdapterBase(
                QueryableAdapter, 
                DataContext, 
                null,
                null);
            operation.Result.Data = new DbList<TEntity>((List<TEntity>) q.ToList());
            return Task.FromResult(operation.Result);
        }
    }
}